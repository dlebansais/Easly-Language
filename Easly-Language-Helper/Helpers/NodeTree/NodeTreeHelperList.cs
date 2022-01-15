namespace BaseNodeHelper;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate lists of nodes in a program tree.
/// </summary>
public static class NodeTreeHelperList
{
    /// <summary>
    /// Checks whether the property of a node is a node list.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the node list upon return.</param>
    /// <returns>True if the property of the node is a node list.</returns>
    public static bool IsNodeListProperty(Node node, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Type NodeType = Node.GetType();

        if (!IsNodeListPropertyInternal(NodeType, PropertyName, out childNodeType))
            return false;

        PropertyInfo Property = SafeType.GetProperty(NodeType, PropertyName);

        object? Item = Property.GetValue(Node);
        Debug.Assert(Item is null || Item is IList);

        return true;
    }

    /// <summary>
    /// Checks whether the property of a node type is a node list.
    /// </summary>
    /// <param name="nodeType">The node type.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the node list upon return.</param>
    /// <returns>True if the property of the node type is a node list.</returns>
    public static bool IsNodeListProperty(Type nodeType, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return IsNodeListPropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Checks whether a list of nodes in a given property of a node contains a child node at the given index.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="index">The index.</param>
    /// <param name="childNode">The child node.</param>
    /// <returns>True if the node list contains <paramref name="childNode"/> at index <paramref name="index"/>; otherwise, false.</returns>
    public static bool IsListChildNode(Node node, string propertyName, int index, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        GetCollection(Node, PropertyName, out _, out _, out IList Collection);

        if (index < 0 || index >= Collection.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Debug.Assert(index >= 0);
        Debug.Assert(index < Collection.Count);

        Node NodeItem = SafeType.ItemAt<Node>(Collection, index);

        return NodeItem == ChildNode;
    }

    /// <summary>
    /// Gets the type of the node list at the given property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The node list type.</returns>
    public static Type ListItemType(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetCollection(Node, PropertyName, out _, out Type PropertyType, out _);

        Debug.Assert(PropertyType.IsGenericType);

        Type[] GenericArguments = PropertyType.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type ChildNodeType = GenericArguments[0];

        Debug.Assert(NodeTreeHelper.IsNodeDescendantType(ChildNodeType));

        return ChildNodeType;
    }

    /// <summary>
    /// Gets the list of nodes from a given property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeList">The node list.</param>
    public static void GetChildNodeList(Node node, string propertyName, out IReadOnlyList<Node> childNodeList)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetCollection(Node, PropertyName, out _, out _, out IList Collection);

        List<Node> NodeList = new();
        foreach (Node Item in SafeType.Items<Node>(Collection))
            NodeList.Add(Item);

        childNodeList = NodeList.AsReadOnly();
    }

    /// <summary>
    /// Clears a list of nodes in a given property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    public static void ClearChildNodeList(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetCollection(Node, PropertyName, out _, out _, out IList Collection);

        Collection.Clear();
    }

    /// <summary>
    /// Gets the last index of a node list at the given property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="lastIndex">The last index.</param>
    public static void GetLastListIndex(Node node, string propertyName, out int lastIndex)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetCollection(Node, PropertyName, out _, out _, out IList Collection);

        lastIndex = Collection.Count;
    }

    /// <summary>
    /// Sets the value of a node list property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeList">The node list.</param>
    public static void SetChildNodeList(Node node, string propertyName, IList childNodeList)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNodeList, out IList ChildNodeList);

        GetCollection(Node, PropertyName, out PropertyInfo Property, out Type PropertyType, out _);

        if (!PropertyType.IsAssignableFrom(ChildNodeList.GetType()))
            throw new ArgumentException($"{nameof(childNodeList)} must conform to type {PropertyType}");

        if (NodeHelper.IsCollectionNeverEmpty(Node, PropertyName) && childNodeList.Count == 0)
            throw new ArgumentException($"Collection '{childNodeList}' must not be empty");

        Property.SetValue(Node, ChildNodeList);
    }

    /// <summary>
    /// Inserts a node into the node list property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="index">The index where to insert.</param>
    /// <param name="childNode">The child node to insert.</param>
    public static void InsertIntoList(Node node, string propertyName, int index, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        GetCollection(Node, PropertyName, out _, out _, out IList Collection);

        if (index < 0 || index > Collection.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Debug.Assert(index >= 0);
        Debug.Assert(index <= Collection.Count);

        Collection.Insert(index, ChildNode);
    }

    /// <summary>
    /// Removes a node from the node list property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="index">The index where to remove.</param>
    public static void RemoveFromList(Node node, string propertyName, int index)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetCollection(Node, PropertyName, out _, out Type PropertyType, out IList Collection);

        if (index < 0 || index >= Collection.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (Collection.Count == 1 && NodeHelper.IsCollectionNeverEmpty(Node, PropertyName))
            throw new NeverEmptyException(Node, PropertyName);

        Debug.Assert(Collection.Count > 0);
        Debug.Assert(index >= 0);
        Debug.Assert(index < Collection.Count);

        Collection.RemoveAt(index);
    }

    /// <summary>
    /// Replaces a node into the node list property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="index">The index where to replace.</param>
    /// <param name="childNode">The child node replacing the existing node.</param>
    public static void ReplaceNode(Node node, string propertyName, int index, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        GetCollection(Node, PropertyName, out _, out Type PropertyType, out IList Collection);

        if (index < 0 || index >= Collection.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Debug.Assert(PropertyType.IsGenericType);

        Type[] GenericArguments = PropertyType.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type GenericType = GenericArguments[0];
        Type ChildNodeType = ChildNode.GetType();

        if (!GenericType.IsAssignableFrom(ChildNodeType))
            throw new ArgumentException($"{nameof(childNode)} must conform to type {GenericType}");

        Debug.Assert(index >= 0);
        Debug.Assert(index < Collection.Count);

        Collection[index] = ChildNode;
    }

    /// <summary>
    /// Moves a node within the node list property of a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="index">The index of the node to move.</param>
    /// <param name="direction">The move direction.</param>
    public static void MoveNode(Node node, string propertyName, int index, int direction)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetCollection(Node, PropertyName, out _, out _, out IList Collection);

        if (index < 0 || index >= Collection.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (index + direction < 0 || index + direction >= Collection.Count)
            throw new ArgumentOutOfRangeException(nameof(direction));

        Debug.Assert(index >= 0);
        Debug.Assert(index + direction >= 0);
        Debug.Assert(index < Collection.Count);
        Debug.Assert(index + direction < Collection.Count);

        Node ChildNode = SafeType.ItemAt<Node>(Collection, index);

        Collection.RemoveAt(index);
        Collection.Insert(index + direction, ChildNode);
    }

    private static bool IsNodeListPropertyInternal(Type nodeType, string propertyName, out Type childNodeType)
    {
        if (SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
        {
            Type PropertyType = Property.PropertyType;

            if (NodeTreeHelper.IsNodeListType(PropertyType))
            {
                Debug.Assert(PropertyType.IsGenericType);
                Type[] GenericArguments = PropertyType.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 1);

                childNodeType = GenericArguments[0];

                return true;
            }
        }

        Contract.Unused(out childNodeType);
        return false;
    }

    private static void GetCollection(Node node, string propertyName, out PropertyInfo property, out Type propertyType, out IList collection)
    {
        Type NodeType = node.GetType();

        if (!SafeType.CheckAndGetPropertyOf(NodeType, propertyName, out property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

        propertyType = property.PropertyType;

        if (!NodeTreeHelper.IsNodeListType(propertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a node list property of {NodeType}");

        collection = SafeType.GetPropertyValue<IList>(property, node);
    }
}
