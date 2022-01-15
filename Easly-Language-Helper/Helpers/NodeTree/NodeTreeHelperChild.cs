namespace BaseNodeHelper;

using System;
using System.Reflection;
using BaseNode;
using Contracts;

/// <summary>
/// Provides methods to manipulate child nodes in a program tree.
/// </summary>
public static class NodeTreeHelperChild
{
    /// <summary>
    /// Checks whether a property of a node is a child node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the node type upon return.</param>
    /// <returns>True if the property is a child node; otherwise, false.</returns>
    public static bool IsChildNodeProperty(Node node, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Type NodeType = Node.GetType();

        return IsChildNodePropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Checks whether a property of a node type is a child node.
    /// </summary>
    /// <param name="nodeType">The node type.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the node type upon return.</param>
    /// <returns>True if the property is a child node; otherwise, false.</returns>
    public static bool IsChildNodeProperty(Type nodeType, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return IsChildNodePropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Checks whether a node is the child of another.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The name of the property to check.</param>
    /// <param name="childNode">The node to check.</param>
    /// <returns>True if <paramref name="childNode"/> is the value at the property of <paramref name="node"/> with name <paramref name="propertyName"/>; otherwise, false.</returns>
    public static bool IsChildNode(Node node, string propertyName, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        Type ParentNodeType = Node.GetType();

        if (SafeType.CheckAndGetPropertyOf(ParentNodeType, PropertyName, out PropertyInfo Property))
        {
            Type PropertyType = Property.PropertyType;

            if (NodeTreeHelper.IsNodeDescendantType(PropertyType))
                if (Property.GetValue(Node) == ChildNode)
                    return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the type of a child node at the given property.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The child node type.</returns>
    public static Type ChildNodeType(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToChildProperty(Node, PropertyName, out _, out Type PropertyType);

        return PropertyType;
    }

    /// <summary>
    /// Gets the child node of a node at the given property.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNode">The child node upon return.</param>
    public static void GetChildNode(Node node, string propertyName, out Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToChildProperty(Node, PropertyName, out PropertyInfo Property, out _);

        childNode = SafeType.GetPropertyValue<Node>(Property, Node);
    }

    /// <summary>
    /// Sets the child node at the given property.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="newChildNode">The new child node.</param>
    public static void SetChildNode(Node node, string propertyName, Node newChildNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(newChildNode, out Node NewChildNode);

        Type NodeType = Node.GetType();

        if (!SafeType.CheckAndGetPropertyOf(NodeType, PropertyName, out PropertyInfo Property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

        Type PropertyType = Property.PropertyType;
        Type ChildNodeType = NewChildNode.GetType();

        if (!PropertyType.IsAssignableFrom(ChildNodeType))
            throw new ArgumentException($"{nameof(newChildNode)} must conform to type {PropertyType}");

        Property.SetValue(Node, NewChildNode);
    }

    private static bool IsChildNodePropertyInternal(Type nodeType, string propertyName, out Type childNodeType)
    {
        if (SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
        {
            Type PropertyType = Property.PropertyType;

            if (NodeTreeHelper.IsNodeDescendantType(PropertyType))
            {
                childNodeType = PropertyType;
                return true;
            }
        }

        Contract.Unused(out childNodeType);
        return false;
    }

    private static void ToChildProperty(Node node, string propertyName, out PropertyInfo property, out Type propertyType)
    {
        Type NodeType = node.GetType();

        if (!SafeType.CheckAndGetPropertyOf(NodeType, propertyName, out property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

        propertyType = property.PropertyType;

        if (!NodeTreeHelper.IsNodeDescendantType(propertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a node property of {NodeType}");
    }
}
