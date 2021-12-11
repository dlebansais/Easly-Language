namespace BaseNodeHelper
{
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();

            if (NodeType == null)
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;

            if (!NodeTreeHelper.IsOptionalReferenceType(PropertyType))
                return false;

            IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);

            if (!Optional.IsAssigned)
                return false;

            Node NodeItem = (Node)Optional.Item;

            if (NodeItem != childNode)
                return false;

            return true;
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
            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);

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
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            isAssigned = false;
            childNode = null!;

            isAssigned = optional.IsAssigned;

            if (optional.HasItem)
                childNode = (Node)optional.Item;
            else
                childNode = null!;

            Debug.Assert(!isAssigned || childNode != null);
        }

        /// <summary>
        /// Gets the optional child node type of a given property in a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The childnode type.</returns>
        public static Type OptionalChildInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(InterfaceType));

            return InterfaceType;
        }

        /// <summary>
        /// Gets the optional reference for a child node of a given property in a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The optional reference.</returns>
        public static IOptionalReference GetOptionalReference(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(Property.PropertyType));

            IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(Property.PropertyType));

            Property.SetValue(node, optional);
        }

        /// <summary>
        /// Sets the child node of a given optional reference property in a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="newChildNode">The child node.</param>
        public static void SetOptionalChildNode(Node node, string propertyName, Node newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type ChildNodeType = newChildNode.GetType();

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];

            // Debug.Assert(ChildNodeType.GetInterface(InterfaceType.FullName) != null);
            Debug.Assert(InterfaceType.IsAssignableFrom(ChildNodeType));

            IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);

            Type OptionalType = Optional.GetType();
            PropertyInfo ItemProperty = SafeType.GetProperty(OptionalType, nameof(OptionalReference<Node>.Item));

            ItemProperty.SetValue(Optional, newChildNode);
            Optional.Assign();
        }

        /// <summary>
        /// Clears the child node of a given optional reference property in a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void ClearOptionalChildNode(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);

            Optional.Clear();

            Debug.Assert(!Optional.HasItem);
        }

        /// <summary>
        /// Checkes whether the child node of a given optional reference property in a node is assigned.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True assigned; otherwise, false.</returns>
        public static bool IsChildNodeAssigned(Node node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            return Optional.IsAssigned;
        }

        /// <summary>
        /// Assigns the child node of a given optional reference property in a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void AssignChildNode(Node node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Assign();
        }

        /// <summary>
        /// Unassigns the child node of a given optional reference property in a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        public static void UnassignChildNode(Node node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Unassign();
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
    }
}
