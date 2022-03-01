namespace BaseNodeHelper;

using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;
using Easly;
using NotNullReflection;
using ArgumentException = System.ArgumentException;
using StringComparison = System.StringComparison;

/// <summary>
/// Provides methods to manipulate a tree of nodes.
/// </summary>
public static partial class NodeTreeHelper
{
    /// <summary>
    /// Enumerates properties of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>The list of property names.</returns>
    public static IList<string> EnumChildNodeProperties(Node node)
    {
        Contract.RequireNotNull(node, out Node Node);

        Type NodeType = Type.FromGetType(Node);
        return EnumChildNodeProperties(NodeType);
    }

    /// <summary>
    /// Enumerates properties of a node type.
    /// </summary>
    /// <param name="nodeType">The node type.</param>
    /// <returns>The list of property names.</returns>
    public static IList<string> EnumChildNodeProperties(Type nodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);

        if (!IsNodeDescendantType(NodeType))
            throw new ArgumentException($"{nameof(nodeType)} must inherit from {nameof(Node)}");

        if (GetBaseNodeAncestor(NodeType, out Type AncestorType))
            NodeType = AncestorType;

        IList<PropertyInfo> Properties = GetTypeProperties(NodeType);

        List<string> Result = new();
        foreach (PropertyInfo Property in Properties)
            if (Property.PropertyType != nodeType)
                Result.Add(Property.Name);

        Result.Sort();

        return Result;
    }

    /// <summary>
    /// Checks whether the provided type inherits from <see cref="Node"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type inherits from <see cref="Node"/>; otherwise, false.</returns>
    public static bool IsNodeDescendantType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        return Type.IsSubclassOf(Type.FromTypeof<Node>());
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="IOptionalReference"/> that inherits from <see cref="Node"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is an optional reference that inherits from <see cref="Node"/>; otherwise, false.</returns>
    public static bool IsOptionalReferenceType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (!Type.IsInterface || !Type.IsGenericType || Type.GetGenericTypeDefinition().Origin != typeof(IOptionalReference<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];

        return IsNodeDescendantType(GenericType);
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="System.Collections.IList"/> of items that inherit from <see cref="Node"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is a <see cref="System.Collections.IList"/> of items that inherit from <see cref="Node"/>; otherwise, false.</returns>
    public static bool IsNodeListType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (!Type.IsInterface || !Type.IsGenericType || Type.GetGenericTypeDefinition().Origin != typeof(IList<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];

        return IsNodeDescendantType(GenericType);
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="BlockList{T}"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is a <see cref="BlockList{T}"/>; otherwise, false.</returns>
    public static bool IsBlockListType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (Type.IsInterface || !Type.IsGenericType || Type.GetGenericTypeDefinition().Origin != typeof(BlockList<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];

        return IsNodeDescendantType(GenericType);
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="IBlockList{T}"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is a <see cref="IBlockList{T}"/>; otherwise, false.</returns>
    public static bool IsBlockListInterfaceType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (!Type.IsInterface || !Type.IsGenericType || Type.GetGenericTypeDefinition().Origin != typeof(IBlockList<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];

        return IsNodeDescendantType(GenericType);
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="Block{T}"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is a <see cref="Block{T}"/>; otherwise, false.</returns>
    public static bool IsBlockType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (Type.IsInterface || !Type.IsGenericType || Type.GetGenericTypeDefinition().Origin != typeof(Block<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];
        Debug.Assert(IsNodeDescendantType(GenericType));

        return true;
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="IBlock{T}"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is a <see cref="IBlock{T}"/>; otherwise, false.</returns>
    public static bool IsBlockInterfaceType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (!Type.IsInterface || !Type.IsGenericType || Type.GetGenericTypeDefinition().Origin != typeof(IBlock<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];
        Debug.Assert(IsNodeDescendantType(GenericType));

        return true;
    }

    /// <summary>
    /// Checks whether the provided type is a <see cref="BlockList{T}"/>, a <see cref="Block{T}"/> or their interface.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>True if the provided type is a <see cref="BlockList{T}"/>, a <see cref="Block{T}"/> or their interface; otherwise, false.</returns>
    public static bool IsSomeBlockType(Type type)
    {
        Contract.RequireNotNull(type, out Type Type);

        if (!Type.IsGenericType)
            return false;

        Type GenericTypeDefinition = Type.GetGenericTypeDefinition();

        if (GenericTypeDefinition.Origin != typeof(Block<>) &&
            GenericTypeDefinition.Origin != typeof(IBlock<>) &&
            GenericTypeDefinition.Origin != typeof(BlockList<>) &&
            GenericTypeDefinition.Origin != typeof(IBlockList<>))
            return false;

        Type[] GenericArguments = Type.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];
        Debug.Assert(IsNodeDescendantType(GenericType));

        return true;
    }

    /// <summary>
    /// Checks whether the provided node contains only text.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>True if the provided node contains only text; otherwise, false.</returns>
    public static bool IsTextNode(Node node)
    {
        Contract.RequireNotNull(node, out Node Node);

        Type NodeType = Type.FromGetType(Node);

        return NodeType.IsProperty(nameof(Identifier.Text), out _);
    }

    /// <summary>
    /// Checks whether <paramref name="node"/> can be assigned <paramref name="childNode"/> in its property with name <paramref name="propertyName"/>.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNode">The child node.</param>
    /// <returns>True if the node can be assigned the child node.</returns>
    public static bool IsAssignable(Node node, string propertyName, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        Type ParentNodeType = Type.FromGetType(Node);

        if (!ParentNodeType.IsProperty(PropertyName, out PropertyInfo Property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {ParentNodeType}");

        Type NodeType = Type.FromGetType(ChildNode);
        Type PropertyType = Property.PropertyType;

        Type AssignedType;

        if (IsNodeDescendantType(PropertyType))
        {
            AssignedType = PropertyType;
        }
        else if (PropertyType.IsGenericType)
        {
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            bool IsKnownGeneric = false;
            IsKnownGeneric |= IsOptionalReferenceType(PropertyType);
            IsKnownGeneric |= IsNodeListType(PropertyType);
            IsKnownGeneric |= IsBlockListInterfaceType(PropertyType);
            Debug.Assert(IsKnownGeneric);

            AssignedType = GenericArguments[0];
        }
        else
            return false;

        if (!AssignedType.IsAssignableFrom(NodeType))
            return false;

        return true;
    }

    private static bool GetBaseNodeAncestor(Type nodeType, out Type ancestorType)
    {
        string FullName = Type.FromTypeof<Node>().FullName;
        string NodeFullName = nodeType.FullName;
        string BaseNodeNamespace = FullName.Substring(0, FullName.IndexOf(".", StringComparison.InvariantCulture) + 1);

        while (!NodeFullName.StartsWith(BaseNodeNamespace, StringComparison.InvariantCulture))
        {
            Debug.Assert(!nodeType.IsTypeof<object>());

            nodeType = nodeType.BaseType;
            NodeFullName = nodeType.FullName;
        }

        if (!nodeType.IsTypeof<Node>())
        {
            ancestorType = nodeType;
            return true;
        }
        else
        {
            Contract.Unused(out ancestorType);
            return false;
        }
    }

    private static IList<PropertyInfo> GetTypeProperties(Type type)
    {
        PropertyInfo[] Properties = type.GetProperties();
        List<PropertyInfo> Result = new(Properties);

        Type[] Interfaces = type.GetInterfaces();
        Debug.Assert(Interfaces.Length == 0);

        return Result;
    }

    /// <summary>
    /// Gets the list of optional child nodes of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="optionalNodesTable">The list of optional child nodes of a node upon return.</param>
    public static void GetOptionalNodes(Node node, out IDictionary<string, IOptionalReference> optionalNodesTable)
    {
        Contract.RequireNotNull(node, out Node Node);

        optionalNodesTable = new Dictionary<string, IOptionalReference>();

        Type NodeType = Type.FromGetType(Node);
        IList<PropertyInfo> Properties = GetTypeProperties(NodeType);

        foreach (PropertyInfo Property in Properties)
        {
            Type PropertyType = Property.PropertyType;
            string PropertyName = Property.Name;

            if (IsOptionalReferenceType(PropertyType))
            {
                IOptionalReference Optional = (IOptionalReference)Property.GetValue(Node);
                optionalNodesTable.Add(PropertyName, Optional);
            }
        }
    }

    /// <summary>
    /// Gets the list of argument blocks of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="argumentBlocksTable">The list of argument blocks of a node upon return.</param>
    public static void GetArgumentBlocks(Node node, out IDictionary<string, IBlockList<Argument>> argumentBlocksTable)
    {
        Contract.RequireNotNull(node, out Node Node);

        argumentBlocksTable = new Dictionary<string, IBlockList<Argument>>();

        Type NodeType = Type.FromGetType(Node);
        IList<PropertyInfo> Properties = GetTypeProperties(NodeType);

        foreach (PropertyInfo Property in Properties)
        {
            Type PropertyType = Property.PropertyType;
            string PropertyName = Property.Name;

            if (IsBlockListInterfaceType(PropertyType))
            {
                Debug.Assert(PropertyType.IsGenericType);
                Type[] GenericArguments = PropertyType.GetGenericArguments();

                Debug.Assert(GenericArguments.Length == 1);

                if (GenericArguments[0].IsTypeof<Argument>())
                {
                    IBlockList<Argument> ArgumentBlocks = (IBlockList<Argument>)Property.GetValue(Node);

                    argumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                }
            }
        }
    }
}
