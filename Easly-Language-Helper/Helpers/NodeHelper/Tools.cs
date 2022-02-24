namespace BaseNodeHelper;

using System.Collections.Generic;
using System.Diagnostics;
using Array = System.Array;
using BindingFlags = System.Reflection.BindingFlags;
using BaseNode;
using Contracts;
using NotNullReflection;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Gets a node type by its short name.
    /// </summary>
    /// <param name="typeName">The type name.</param>
    /// <param name="type">The type upon return.</param>
    /// <returns>True if the type could be found; otherwise, false.</returns>
    public static bool GetNodeType(string typeName, out Type type)
    {
        string RootName = Type.FromTypeof<Root>().FullName;

        int Index = RootName.LastIndexOf('.');
        string FullTypeName = RootName.Substring(0, Index + 1) + typeName;

        Assembly RootAssembly = Type.FromTypeof<Root>().Assembly;

        if (RootAssembly.HasType(FullTypeName, out Type FullType))
        {
            type = FullType;
            return true;
        }

        Contract.Unused(out type);
        return false;
    }

    /// <summary>
    /// Get keys to create a dictionary of nodes indexed by types.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static IList<Type> GetNodeKeys()
    {
        List<Type> Result = new();
        Assembly LanguageAssembly = Type.FromTypeof<Root>().Assembly;
        Type[] LanguageTypes = LanguageAssembly.GetTypes();

        foreach (Type Item in LanguageTypes)
            if (!Item.IsInterface && !Item.IsAbstract && Item.IsSubclassOf(Type.FromTypeof<Node>()) && Item.IsPublic)
                Result.Add(Item);

        return Result;
    }

    /// <summary>
    /// Checks whether a property of a node is a collection of blocks that must never be empty.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the property is a collection of blocks that must never be empty; otherwise, false.</returns>
    public static bool IsCollectionNeverEmpty(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return IsCollectionNeverEmpty(Type.FromGetType(Node), PropertyName);
    }

    /// <summary>
    /// Checks whether a property of a node type is a collection of blocks that must never be empty.
    /// </summary>
    /// <param name="nodeType">The node type.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the property is a collection of blocks that must never be empty; otherwise, false.</returns>
    public static bool IsCollectionNeverEmpty(Type nodeType, string propertyName)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Debug.Assert(NodeTreeHelperList.IsNodeListProperty(NodeType, PropertyName, out _) || NodeTreeHelperBlockList.IsBlockListProperty(NodeType, PropertyName, out _));

        bool Result = false;

        if (NeverEmptyCollectionTable.ContainsKey(NodeType))
            Result = Array.Exists(NeverEmptyCollectionTable[NodeType], (string item) => item == PropertyName);

        return Result;
    }

    /// <summary>
    /// Checks whether a property of a node is a collection of blocks that can be expanded.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the property is a collection of blocks that can be expanded; otherwise, false.</returns>
    public static bool IsCollectionWithExpand(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return IsCollectionWithExpand(Type.FromGetType(Node), PropertyName);
    }

    /// <summary>
    /// Checks whether a property of a node type is a collection of blocks that can be expanded.
    /// </summary>
    /// <param name="nodeType">The node type.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the property is a collection of blocks that can be expanded; otherwise, false.</returns>
    public static bool IsCollectionWithExpand(Type nodeType, string propertyName)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Debug.Assert(NodeTreeHelperBlockList.IsBlockListProperty(NodeType, PropertyName, out _));
        Debug.Assert(!IsCollectionNeverEmpty(NodeType, PropertyName));

        bool Result = false;

        if (WithExpandCollectionTable.ContainsKey(NodeType))
            Result = Array.Exists(WithExpandCollectionTable[NodeType], (string item) => item == PropertyName);

        return Result;
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Document"/> as a copy of the provided value.
    /// </summary>
    /// <param name="document">The documentation to copy.</param>
    /// <returns>The created instance.</returns>
    public static Document CreateDocumentationCopy(Document document)
    {
        Contract.RequireNotNull(document, out Document Document);

        return CreateSimpleDocument(Document.Comment, Document.Uuid);
    }

    /// <summary>
    /// Creates a new instance of a <see cref="QualifiedName"/> from a string.
    /// </summary>
    /// <param name="text">The string.</param>
    /// <returns>The created instance.</returns>
    private static QualifiedName StringToQualifiedName(string text)
    {
        string[] StringList;
        ParseDotSeparatedIdentifiers(text, out StringList);

        List<Identifier> IdentifierList = new();
        foreach (string Identifier in StringList)
            IdentifierList.Add(CreateSimpleIdentifier(Identifier));

        return CreateQualifiedName(IdentifierList);
    }

    private static void ParseDotSeparatedIdentifiers(string text, out string[] stringList)
    {
        ParseSymbolSeparatedStrings(text, '.', out stringList);
    }

    private static void ParseSymbolSeparatedStrings(string text, char symbol, out string[] stringList)
    {
        string[] SplittedStrings = text.Split(symbol);
        stringList = new string[SplittedStrings.Length];
        for (int i = 0; i < SplittedStrings.Length; i++)
            stringList[i] = SplittedStrings[i].Trim();
    }

    private static bool GetExpressionText(Expression expressionNode, out string text)
    {
        if (expressionNode is ManifestNumberExpression AsNumber)
        {
            text = AsNumber.Text;
            return true;
        }
        else if (expressionNode is QueryExpression AsQuery)
        {
            text = AsQuery.Query.Path[0].Text;
            return true;
        }
        else
        {
            Contract.Unused(out text);
            return false;
        }
    }

    /// <summary>
    /// Creates an instance of the specified type from this assembly using the system activator.
    /// The specified type must have a constructor.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="t">The type of the instance to create.</param>
    /// <returns>An instance of the specified type created with the default constructor.</returns>
    internal static T CreateInstance<T>(Type t)
        where T : class
    {
        ConstructorInfo[] Constructors = t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

#if !NO_PARAMETERLESS_CONSTRUCTOR
        Debug.Assert(Constructors.Length > 0);

        int ParameterCount = Constructors[Constructors.Length - 1].GetParameters().Length;
        object[] Arguments = new object[ParameterCount];

        T? Result = System.Activator.CreateInstance(t.Origin, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, binder: default, Arguments, culture: default) as T;
#else
        Debug.Assert(Constructors.Length == 1);

        int ParameterCount = Constructors[0].GetParameters().Length;
        object[] Arguments = new object[ParameterCount];

        T? Result = Activator.CreateInstance(Type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, binder: default, Arguments, culture: default) as T;
#endif

        return Contract.NullSupressed(Result);
    }

    /// <summary>
    /// Creates an instance of the specified type from this assembly using the system activator.
    /// The specified type must have a default constructor.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="t">The type of the instance to create.</param>
    /// <returns>An instance of the specified type created with the default constructor.</returns>
    internal static T CreateInstanceFromDefaultConstructor<T>(Type t)
        where T : class
    {
        Debug.Assert(TypeHasDefaultConstructor<T>(t));

        T? Result = t.Assembly.CreateInstance(t.FullName) as T;
        return Contract.NullSupressed(Result);
    }

    /// <summary>
    /// Checkcs that a type has a default constructor.
    /// </summary>
    /// <typeparam name="T">The type constrait.</typeparam>
    /// <param name="t">The type of the instance to create.</param>
    /// <returns>True if the specified type has a default constructor; otherwise, false.</returns>
    internal static bool TypeHasDefaultConstructor<T>(Type t)
        where T : class
    {
        Type Type = Contract.NullSupressed(t.Assembly.GetType(t.FullName));
        ConstructorInfo[] Constructors = Type.GetConstructors();

        Debug.Assert(Constructors.Length > 0);

        return Constructors[0].GetParameters().Length == 0;
    }
}
