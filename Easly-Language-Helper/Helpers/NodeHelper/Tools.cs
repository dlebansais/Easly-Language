namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;
using Contracts;

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
        string RootName = SafeType.FullName(typeof(Root));

        int Index = RootName.LastIndexOf('.');
        string FullTypeName = RootName.Substring(0, Index + 1) + typeName;

        Assembly RootAssembly = typeof(Root).Assembly;

        Type? FullType = RootAssembly.GetType(FullTypeName);

        if (FullType is not null)
        {
            type = FullType;
            return true;
        }

        Contract.Unused(out type);
        return false;
    }

    /// <summary>
    /// Creates a dictionary of nodes indexed by types.
    /// </summary>
    /// <typeparam name="TValue">The dictionary value type.</typeparam>
    /// <param name="defaultValue">The default value to associate to each type.</param>
    /// <returns>The created instance.</returns>
    public static IDictionary<Type, TValue> CreateNodeDictionary<TValue>(TValue defaultValue)
    {
        IDictionary<Type, TValue> Result = new Dictionary<Type, TValue>();
        Assembly LanguageAssembly = typeof(Root).Assembly;
        Type[] LanguageTypes = LanguageAssembly.GetTypes();

        foreach (Type Item in LanguageTypes)
            if (!Item.IsInterface && !Item.IsAbstract && Item.IsSubclassOf(typeof(Node)))
                Result.Add(Item, defaultValue);

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

        return IsCollectionNeverEmpty(Node.GetType(), PropertyName);
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

        return IsCollectionWithExpand(Node.GetType(), PropertyName);
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
    /// <param name="documentation">The documentation to copy.</param>
    /// <returns>The created instance.</returns>
    public static Document CreateDocumentationCopy(Document documentation)
    {
        Contract.RequireNotNull(documentation, out Document Documentation);

        return CreateSimpleDocumentation(Documentation.Comment, documentation.Uuid);
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
}
