namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;
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
            return IsChildNodeProperty(NodeType, PropertyName, out childNodeType);
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

            if (SafeType.CheckAndGetPropertyOf(NodeType, PropertyName, out PropertyInfo Property))
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
        public static Type ChildInterfaceType(Node node, string propertyName)
        {
            Contract.RequireNotNull(node, out Node Node);
            Contract.RequireNotNull(propertyName, out string PropertyName);

            Type NodeType = Node.GetType();

            if (!SafeType.CheckAndGetPropertyOf(NodeType, PropertyName, out PropertyInfo Property))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

            Type PropertyType = Property.PropertyType;

            if (!NodeTreeHelper.IsNodeDescendantType(PropertyType))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a node property of {NodeType}");

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

            Type NodeType = Node.GetType();

            if (!SafeType.CheckAndGetPropertyOf(NodeType, PropertyName, out PropertyInfo Property))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType}");

            Type PropertyType = Property.PropertyType;

            if (!NodeTreeHelper.IsNodeDescendantType(PropertyType))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a node property of {NodeType}");

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

            Type ChildNodeType = NewChildNode.GetType();

            if (!Property.PropertyType.IsAssignableFrom(ChildNodeType))
                throw new ArgumentException($"{nameof(propertyName)} must be the name of a property of {NodeType} of type {ChildNodeType}");

            Property.SetValue(Node, NewChildNode);
        }
    }
}
