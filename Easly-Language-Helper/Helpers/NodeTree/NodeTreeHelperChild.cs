namespace BaseNodeHelper
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            return IsChildNodeProperty(node.GetType(), propertyName, out childNodeType);
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
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null!;

            if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
                return false;

            Type PropertyType = Property.PropertyType;

            // if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
            if (!NodeTreeHelper.IsNodeDescendantType(PropertyType))
                return false;

            childNodeType = PropertyType;
            return true;
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();

            PropertyInfo? Property = ParentNodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;

            // if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
            if (!NodeTreeHelper.IsNodeDescendantType(PropertyType))
                return false;

            if (Property.GetValue(node) != childNode)
                return false;

            return true;
        }

        /// <summary>
        /// Gets the child node of a node at the given property.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="childNode">The child node upon return.</param>
        public static void GetChildNode(Node node, string propertyName, out Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(PropertyType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(PropertyType));

            Node Result = SafeType.GetPropertyValue<Node>(Property, node);

            childNode = Result;
        }

        /// <summary>
        /// Gets the type of a child node at the given property.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The child node type.</returns>
        public static Type ChildInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type InterfaceType = Property.PropertyType;

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(InterfaceType));

            return InterfaceType;
        }

        /// <summary>
        /// Sets the child node at the given property.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="newChildNode">The new child node.</param>
        public static void SetChildNode(Node node, string propertyName, Node newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(PropertyType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(PropertyType));

            Type ChildNodeType = newChildNode.GetType();

            // Debug.Assert(ChildNodeType.GetInterface(Property.PropertyType.FullName) != null);
            Debug.Assert(Property.PropertyType.IsAssignableFrom(ChildNodeType));

            Property.SetValue(node, newChildNode);
        }
    }
}
