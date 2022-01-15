namespace BaseNodeHelper;
using System;
using System.Diagnostics;
using System.Reflection;
using BaseNode;
using Contracts;
using Easly;

/// <summary>
/// Provides methods to manipulate optional nodes in a program tree.
/// </summary>
public static class NodeTreeHelperOptional
{
    /// <summary>
    /// Checks whether a property of a node is that of an optional child node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the child node type upon return.</param>
    /// <returns>True if the property is that of an optional child node; otherwise, false.</returns>
    public static bool IsOptionalChildNodeProperty(Node node, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        Type NodeType = Node.GetType();

        return IsOptionalChildNodePropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Checks whether a property of a node type is that of an optional child node.
    /// </summary>
    /// <param name="nodeType">The node type.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNodeType">If successful, the child node type upon return.</param>
    /// <returns>True if the property is that of an optional child node; otherwise, false.</returns>
    public static bool IsOptionalChildNodeProperty(Type nodeType, string propertyName, out Type childNodeType)
    {
        Contract.RequireNotNull(nodeType, out Type NodeType);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        return IsOptionalChildNodePropertyInternal(NodeType, PropertyName, out childNodeType);
    }

    /// <summary>
    /// Checks whether a child node is the value of an optional node property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="childNode">The child node.</param>
    /// <returns>True if <paramref name="childNode"/> is the value of an optional node property in <paramref name="node"/>; otherwise, false.</returns>
    public static bool IsOptionalChildNode(Node node, string propertyName, Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        if (!Optional.IsAssigned)
            return false;

        Node NodeItem = (Node)Optional.Item;

        return NodeItem == ChildNode;
    }

    /// <summary>
    /// Gets the optional child node of a given property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="isAssigned">A value indicating whether the child node is assigned upon return.</param>
    /// <param name="hasItem">A value indicating whether there is a child node upon return.</param>
    /// <param name="childNode">The child node upon return.</param>
    public static void GetChildNode(Node node, string propertyName, out bool isAssigned, out bool hasItem, out Node childNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        isAssigned = Optional.IsAssigned;
        hasItem = Optional.HasItem;

        if (hasItem)
            childNode = (Node)Optional.Item;
        else
        {
            Debug.Assert(!isAssigned);

            Contract.Unused(out childNode);
        }
    }

    /// <summary>
    /// Gets the optional child node from an instance of a <see cref="IOptionalReference"/> object.
    /// </summary>
    /// <param name="optional">The source object.</param>
    /// <param name="isAssigned">A value indicating whether the child node is assigned upon return.</param>
    /// <param name="childNode">The child node upon return.</param>
    public static void GetChildNode(IOptionalReference optional, out bool isAssigned, out Node childNode)
    {
        Contract.RequireNotNull(optional, out IOptionalReference Optional);

        isAssigned = Optional.IsAssigned;

        if (Optional.HasItem)
            childNode = (Node)Optional.Item;
        else
        {
            Debug.Assert(!isAssigned);

            Contract.Unused(out childNode);
        }
    }

    /// <summary>
    /// Gets the optional child node type of a given property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The childnode type.</returns>
    public static Type OptionalItemType(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out Type PropertyType, out _);

        Debug.Assert(PropertyType.IsGenericType);

        Type[] GenericArguments = PropertyType.GetGenericArguments();
        Debug.Assert(GenericArguments.Length == 1);

        Type ChildNodeType = GenericArguments[0];

        Debug.Assert(NodeTreeHelper.IsNodeDescendantType(ChildNodeType));

        return ChildNodeType;
    }

    /// <summary>
    /// Gets the optional reference for a child node of a given property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The optional reference.</returns>
    public static IOptionalReference GetOptionalReference(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        return Optional;
    }

    /// <summary>
    /// Sets the optional reference for a child node of a given property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="optional">The optional reference.</param>
    public static void SetOptionalReference(Node node, string propertyName, IOptionalReference optional)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(optional, out IOptionalReference Optional);

        ToOptionalChildProperty(Node, PropertyName, out PropertyInfo Property, out _, out _);

        Property.SetValue(Node, Optional);
    }

    /// <summary>
    /// Sets the child node of a given optional reference property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="newChildNode">The child node.</param>
    public static void SetOptionalChildNode(Node node, string propertyName, Node newChildNode)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(newChildNode, out Node NewChildNode);

        ToOptionalChildProperty(Node, PropertyName, out _, out Type PropertyType, out IOptionalReference Optional);

        Debug.Assert(PropertyType.IsGenericType);
        Type[] GenericArguments = PropertyType.GetGenericArguments();

        Debug.Assert(GenericArguments.Length == 1);

        Type ChildNodeType = GenericArguments[0];
        Type NewChildNodeType = NewChildNode.GetType();

        if (!ChildNodeType.IsAssignableFrom(NewChildNodeType))
            throw new ArgumentException($"{nameof(newChildNode)} must conform to type {ChildNodeType}");

        Type OptionalType = Optional.GetType();
        PropertyInfo ItemProperty = SafeType.GetProperty(OptionalType, nameof(OptionalReference<Node>.Item));

        ItemProperty.SetValue(Optional, NewChildNode);
        Optional.Assign();
    }

    /// <summary>
    /// Checkes whether the child node of a given optional reference property in a node is assigned.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    /// <returns>True assigned; otherwise, false.</returns>
    public static bool IsChildNodeAssigned(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        return Optional.IsAssigned;
    }

    /// <summary>
    /// Assigns the child node of a given optional reference property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    public static void AssignChildNode(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        Optional.Assign();
    }

    /// <summary>
    /// Unassigns the child node of a given optional reference property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    public static void UnassignChildNode(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        Optional.Unassign();
    }

    /// <summary>
    /// Clears the child node of a given optional reference property in a node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="propertyName">The property name.</param>
    public static void ClearOptionalChildNode(Node node, string propertyName)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        ToOptionalChildProperty(Node, PropertyName, out _, out _, out IOptionalReference Optional);

        Optional.Clear();

        Debug.Assert(!Optional.HasItem);
    }

    private static bool IsOptionalChildNodePropertyInternal(Type nodeType, string propertyName, out Type childNodeType)
    {
        if (SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
        {
            Type PropertyType = Property.PropertyType;

            if (NodeTreeHelper.IsOptionalReferenceType(PropertyType))
            {
                Debug.Assert(PropertyType.IsGenericType);

                Type[] GenericArguments = PropertyType.GetGenericArguments();
                Debug.Assert(GenericArguments.Length == 1);

                childNodeType = GenericArguments[0];

                return NodeTreeHelper.IsNodeDescendantType(childNodeType);
            }
        }

        Contract.Unused(out childNodeType);
        return false;
    }

    private static void ToOptionalChildProperty(Node node, string propertyName, out PropertyInfo property, out Type propertyType, out IOptionalReference optional)
    {
        Type NodeType = node.GetType();

        if (!SafeType.CheckAndGetPropertyOf(NodeType, propertyName, out property))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

        propertyType = property.PropertyType;

        if (!NodeTreeHelper.IsOptionalReferenceType(propertyType))
            throw new ArgumentException($"{nameof(propertyName)} must be the name of an optional node property of {NodeType}");

        optional = SafeType.GetPropertyValue<IOptionalReference>(property, node);
    }
}
