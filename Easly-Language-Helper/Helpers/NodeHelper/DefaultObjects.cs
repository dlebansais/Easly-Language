namespace BaseNodeHelper;

using System.Collections.Generic;
using System.Diagnostics;
using Guid = System.Guid;
using ArgumentException = System.ArgumentException;
using BindingFlags = System.Reflection.BindingFlags;
using BaseNode;
using Contracts;
using NotNullReflection;
using System.Globalization;

/// <summary>
/// Provides methods to manipulate nodes.
/// </summary>
public static partial class NodeHelper
{
    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Argument"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Argument CreateDefaultArgument()
    {
        return CreateEmptyPositionalArgument();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="TypeArgument"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static TypeArgument CreateDefaultTypeArgument()
    {
        return CreateEmptyPositionalTypeArgument();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Body"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Body CreateDefaultBody()
    {
        return CreateEmptyEffectiveBody();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Expression CreateDefaultExpression()
    {
        return CreateEmptyQueryExpression();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Instruction CreateDefaultInstruction()
    {
        return CreateEmptyCommandInstruction();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Feature"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static Feature CreateDefaultFeature()
    {
        return CreateEmptyAttributeFeature();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="ObjectType"/> with the simplest content.
    /// </summary>
    /// <returns>The created instance.</returns>
    public static ObjectType CreateDefaultObjectType()
    {
        return CreateEmptySimpleType();
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Node"/> with the simplest content that inherits from <paramref name="nodeType"/>.
    /// <paramref name="nodeType"/> must be one of the supported types.
    /// </summary>
    /// <param name="nodeType">The required type the new object must inherit from.</param>
    /// <returns>The created instance.</returns>
    public static Node CreateDefault(Type nodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);

        if (!CreateDefaultNoCheck(NodeType, out Node Result))
            throw new ArgumentException($"No default object can be created for {nodeType.FullName}");

        return Result;
    }

    /// <summary>
    /// Gets the type of the simplest object inheriting from <see cref="Node"/> that implements <paramref name="nodeType"/>.
    /// </summary>
    /// <param name="nodeType">The required interface the returned type must implement.</param>
    /// <returns>The type of the simplest object inheriting from <see cref="Node"/> that implements <paramref name="nodeType"/>.</returns>
    public static Type GetDefaultItemType(Type nodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);

        if (NodeType.IsTypeof<Argument>())
            return Type.FromTypeof<PositionalArgument>();
        else if (NodeType.IsTypeof<TypeArgument>())
            return Type.FromTypeof<PositionalTypeArgument>();
        else if (NodeType.IsTypeof<Body>())
            return Type.FromTypeof<EffectiveBody>();
        else if (NodeType.IsTypeof<Expression>())
            return Type.FromTypeof<QueryExpression>();
        else if (NodeType.IsTypeof<Instruction>())
            return Type.FromTypeof<CommandInstruction>();
        else if (NodeType.IsTypeof<Feature>())
            return Type.FromTypeof<AttributeFeature>();
        else if (NodeType.IsTypeof<ObjectType>())
            return Type.FromTypeof<SimpleType>();
        else
            return NodeType;
    }

    /// <summary>
    /// Creates a new instance of an object inheriting from <see cref="Node"/> with the simplest content that implements <paramref name="nodeType"/>.
    /// </summary>
    /// <param name="nodeType">The required type the new object must inherit from.</param>
    /// <returns>The created instance.</returns>
    public static Node CreateDefaultFromType(Type nodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);

        if (CreateDefaultNoCheck(NodeType, out Node Result))
            return Result;

        string NodeTypeName = NodeType.AssemblyQualifiedName;
        Type ResultNodeType = Type.GetType(NodeTypeName);

        Debug.Assert(!ResultNodeType.IsAbstract, $"A default type value is never abstract");

        Result = CreateEmptyNode(ResultNodeType);
        return Result;
    }

    /// <summary>
    /// Checks if <paramref name="type"/> is a node type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if <paramref name="type"/> is a node type; otherwise, false.</returns>
    public static bool IsNodeType(Type type)
    {
        Type CurrentType = type;

        while (!CurrentType.IsTypeof<Node>() && !CurrentType.IsTypeof<object>())
            CurrentType = CurrentType.BaseType;

        return CurrentType.IsTypeof<Node>();
    }

    /// <summary>
    /// Creates a new instance of an object of type <paramref name="objectType"/>.
    /// </summary>
    /// <param name="objectType">The type of the new object to create.</param>
    /// <returns>The created instance.</returns>
    public static Node CreateEmptyNode(Type objectType)
    {
        if (!IsNodeType(objectType))
            throw new ArgumentException($"{nameof(objectType)} must be a node type");

        if (objectType.IsAbstract)
            throw new ArgumentException($"{nameof(objectType)} must not be an abstract node type");

        string FullName = objectType.FullName;
        ConstructorInfo[] Constructors = objectType.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        int ParameterCount = Constructors[Constructors.Length - 1].GetParameters().Length;
        object[] Arguments = new object[ParameterCount];
        Node EmptyNode = (Node)objectType.Assembly.CreateInstance(objectType.FullName, ignoreCase: false, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Assembly.DefaultBinder, Arguments, CultureInfo.InvariantCulture);

        IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(EmptyNode);

        foreach (string PropertyName in PropertyNames)
            CreateEmptyNodePropertyName(objectType, EmptyNode, PropertyName);

        InitializeDocumentation(EmptyNode);

        return EmptyNode;
    }

    /// <summary>
    /// Fills the content of the <paramref name="propertyName"/> property of <paramref name="emptyNode"/> with an empty content, using <paramref name="objectType"/> as the item type if the property is a collection.
    /// </summary>
    /// <param name="objectType">The item type if the property is a collection.</param>
    /// <param name="emptyNode">The node to fill.</param>
    /// <param name="propertyName">The name of the property in <paramref name="emptyNode"/> to fill.</param>
    public static void CreateEmptyNodePropertyName(Type objectType, Node emptyNode, string propertyName)
    {
        Type ChildNodeType;

        if (NodeTreeHelperChild.IsChildNodeProperty(emptyNode, propertyName, out ChildNodeType))
            InitializeChildNode(emptyNode, propertyName, CreateDefaultFromType(ChildNodeType));
        else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(emptyNode, propertyName, out _))
            InitializeUnassignedOptionalChildNode(emptyNode, propertyName);
        else if (NodeTreeHelperList.IsNodeListProperty(emptyNode, propertyName, out ChildNodeType))
            CreateEmptyNodeList(objectType, emptyNode, propertyName, ChildNodeType);
        else if (NodeTreeHelperBlockList.IsBlockListProperty(emptyNode, propertyName, out ChildNodeType))
            CreateEmptyBlockList(objectType, emptyNode, propertyName, ChildNodeType);
        else if (NodeTreeHelper.IsStringProperty(emptyNode, propertyName))
            NodeTreeHelper.SetStringProperty(emptyNode, propertyName, string.Empty);
        else if (NodeTreeHelper.IsGuidProperty(emptyNode, propertyName))
            NodeTreeHelper.SetGuidProperty(emptyNode, propertyName, Guid.NewGuid());
    }

    /// <summary>
    /// Creates an empty list of <paramref name="childNodeType"/> elements in the property with name <paramref name="propertyName"/> of the <paramref name="emptyNode"/> node.
    /// </summary>
    /// <param name="objectType">Not used.</param>
    /// <param name="emptyNode">The node to fill.</param>
    /// <param name="propertyName">The name of the property in <paramref name="emptyNode"/> to fill.</param>
    /// <param name="childNodeType">The type of element elements in the created list.</param>
    private static void CreateEmptyNodeList(Type objectType, Node emptyNode, string propertyName, Type childNodeType)
    {
        if (IsCollectionNeverEmpty(emptyNode, propertyName))
        {
            Debug.Assert(!childNodeType.IsAbstract, "There isn't any list of abstract nodes in the language");

            Node FirstNode = CreateDefaultFirstNode(childNodeType);
            InitializeSimpleNodeList(emptyNode, propertyName, childNodeType, FirstNode);
        }
        else
            InitializeEmptyNodeList(emptyNode, propertyName, childNodeType);
    }

    /// <summary>
    /// Creates an empty block list of <paramref name="childNodeType"/> elements in the property with name <paramref name="propertyName"/> of the <paramref name="emptyNode"/> node.
    /// If the block list is not allowed to be empty, it contains an empty element upon return.
    /// </summary>
    /// <param name="objectType">Not used.</param>
    /// <param name="emptyNode">The node to fill.</param>
    /// <param name="propertyName">The name of the property in <paramref name="emptyNode"/> to fill.</param>
    /// <param name="childNodeType">The type of element elements in the created list.</param>
    private static void CreateEmptyBlockList(Type objectType, Node emptyNode, string propertyName, Type childNodeType)
    {
        if (IsCollectionNeverEmpty(emptyNode, propertyName))
        {
            Node FirstNode = CreateDefaultFirstNode(childNodeType);
            InitializeSimpleBlockList(emptyNode, propertyName, childNodeType, FirstNode);
        }
        else
            InitializeEmptyBlockList(emptyNode, propertyName, childNodeType);
    }

    private static Node CreateDefaultFirstNode(Type nodeType)
    {
        Node FirstNode;

        if (nodeType.IsAbstract)
            FirstNode = CreateDefault(nodeType);
        else
            FirstNode = CreateEmptyNode(nodeType);

        return FirstNode;
    }

    /// <summary>
    /// Checks if a node is empty.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>True if the node is empty; otherwise, false.</returns>
    public static bool IsEmptyNode(Node node)
    {
        Contract.RequireNotNull(node, out Node Node);

        IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(Type.FromGetType(Node));

        foreach (string PropertyName in PropertyNames)
            if (!IsEmptyNodePropertyName(Node, PropertyName))
                return false;

        return true;
    }

    /// <summary>
    /// Checks if the property of a node is empty.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is empty; otherwise, false.</returns>
    private static bool IsEmptyNodePropertyName(Node node, string propertyName)
    {
        if (NodeTreeHelperChild.IsChildNodeProperty(node, propertyName, out _))
            return IsEmptyChildNode(node, propertyName);
        else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, propertyName, out _))
            return IsEmptyOptionalChildNode(node, propertyName);
        else if (NodeTreeHelperList.IsNodeListProperty(node, propertyName, out _))
            return IsEmptyNodeList(node, propertyName);
        else if (NodeTreeHelperBlockList.IsBlockListProperty(node, propertyName, out _))
            return IsEmptyBlockList(node, propertyName);
        else if (NodeTreeHelper.IsStringProperty(node, propertyName))
            return IsEmptyStringProperty(node, propertyName);
        else if (NodeTreeHelper.IsBooleanProperty(node, propertyName) || NodeTreeHelper.IsEnumProperty(node, propertyName))
            return IsEmptyEnumProperty(node, propertyName);
        else
            return true;
    }

    /// <summary>
    /// Checks if the property of a node is an empty child node.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is an empty child node; otherwise, false.</returns>
    private static bool IsEmptyChildNode(Node node, string propertyName)
    {
        NodeTreeHelperChild.GetChildNode(node, propertyName, out Node ChildNode);
        return IsEmptyNode(ChildNode);
    }

    /// <summary>
    /// Checks if the property of a node is an unassigned optional child node.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is an unassigned optional child node; otherwise, false.</returns>
    private static bool IsEmptyOptionalChildNode(Node node, string propertyName)
    {
        NodeTreeHelperOptional.GetChildNode(node, propertyName, out bool IsAssigned, out _);
        return !IsAssigned;
    }

    /// <summary>
    /// Checks if the property of a node is an empty list of nodes.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is an empty list of nodes; otherwise, false.</returns>
    private static bool IsEmptyNodeList(Node node, string propertyName)
    {
        NodeTreeHelperList.GetChildNodeList(node, propertyName, out IReadOnlyList<Node> ChildNodeList);

        if (IsCollectionNeverEmpty(node, propertyName))
        {
            Debug.Assert(ChildNodeList.Count > 0, $"A collection that is found not empty has to have an element");

            if (ChildNodeList.Count != 1)
                return false;

            Node ChildNode = ChildNodeList[0];
            if (!IsEmptyNode(ChildNode))
                return false;
        }
        else if (ChildNodeList.Count > 0)
            return false;

        return true;
    }

    /// <summary>
    /// Checks if the property of a node is an empty block list.
    /// If the block list is not allowed to be empty, returns true if it contains only one default element.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is an empty block list; otherwise, false.</returns>
    private static bool IsEmptyBlockList(Node node, string propertyName)
    {
        NodeTreeHelperBlockList.GetChildBlockList(node, propertyName, out IList<NodeTreeBlock> ChildBlockList);

        if (IsCollectionNeverEmpty(node, propertyName))
        {
            Debug.Assert(ChildBlockList.Count > 0, $"A collection that is found not empty has to have an element");

            if (ChildBlockList.Count != 1)
                return false;

            NodeTreeBlock FirstBlock = ChildBlockList[0];
            Debug.Assert(FirstBlock.NodeList.Count > 0, $"Blocks in block lists always have at least one node");

            if (FirstBlock.NodeList.Count != 1)
                return false;

            Node ChildNode = FirstBlock.NodeList[0];
            if (!IsEmptyNode(ChildNode))
                return false;
        }
        else if (ChildBlockList.Count > 0)
            return false;

        return true;
    }

    /// <summary>
    /// Checks if the property of a node is an empty string.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is an empty string; otherwise, false.</returns>
    private static bool IsEmptyStringProperty(Node node, string propertyName)
    {
        string Text = NodeTreeHelper.GetString(node, propertyName);
        return Text.Length == 0;
    }

    /// <summary>
    /// Checks if the property of a node is an enum with default value.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True if the node property is an enum with default value; otherwise, false.</returns>
    private static bool IsEmptyEnumProperty(Node node, string propertyName)
    {
        int Value = NodeTreeHelper.GetEnumValue(node, propertyName);
        NodeTreeHelper.GetEnumRange(Type.FromGetType(node), propertyName, out int Min, out _);

        return Value == Min;
    }

    private static bool CreateDefaultNoCheck(Type nodeType, out Node node)
    {
        if (nodeType.IsTypeof<Body>() || nodeType.IsTypeof<EffectiveBody>())
            node = CreateDefaultBody();
        else if (nodeType.IsTypeof<Expression>() || nodeType.IsTypeof<QueryExpression>())
            node = CreateDefaultExpression();
        else if (nodeType.IsTypeof<Instruction>() || nodeType.IsTypeof<CommandInstruction>())
            node = CreateDefaultInstruction();
        else if (nodeType.IsTypeof<Feature>() || nodeType.IsTypeof<AttributeFeature>())
            node = CreateDefaultFeature();
        else if (nodeType.IsTypeof<ObjectType>() || nodeType.IsTypeof<SimpleType>())
            node = CreateDefaultObjectType();
        else
            return CreateDefaultNoCheckArgument(nodeType, out node);

        return true;
    }

    private static bool CreateDefaultNoCheckArgument(Type nodeType, out Node node)
    {
        if (nodeType.IsTypeof<Argument>() || nodeType.IsTypeof<PositionalArgument>())
            node = CreateDefaultArgument();
        else if (nodeType.IsTypeof<TypeArgument>() || nodeType.IsTypeof<PositionalTypeArgument>())
            node = CreateDefaultTypeArgument();
        else
            return CreateDefaultNoCheckSingle(nodeType, out node);

        return true;
    }

    private static bool CreateDefaultNoCheckSingle(Type nodeType, out Node node)
    {
        if (nodeType.IsTypeof<Name>())
            node = CreateEmptyName();
        else if (nodeType.IsTypeof<Identifier>())
            node = CreateEmptyIdentifier();
        else if (nodeType.IsTypeof<QualifiedName>())
            node = CreateEmptyQualifiedName();
        else if (nodeType.IsTypeof<Scope>())
            node = CreateEmptyScope();
        else if (nodeType.IsTypeof<Import>())
            node = CreateSimpleImport(string.Empty, string.Empty, ImportType.Latest);
        else
        {
            Contract.Unused(out node);
            return false;
        }

        return true;
    }
}
