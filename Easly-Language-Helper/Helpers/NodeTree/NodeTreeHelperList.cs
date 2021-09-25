namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();

            if (!IsNodeListProperty(NodeType, propertyName, out childNodeType))
                return false;

            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            object? Collection = Property.GetValue(node);
            Debug.Assert(Collection == null || Collection is IList);

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
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null!;

            if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsNodeListType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];

            // return NodeTreeHelper.IsNodeInterfaceType(childNodeType);
            return NodeTreeHelper.IsNodeDescendantType(childNodeType);
        }

        /// <summary>
        /// Gets the list of nodes from a given property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="childNodeList">The node list.</param>
        public static void GetChildNodeList(Node node, string propertyName, out IReadOnlyList<Node> childNodeList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            childNodeList = null!;

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            List<Node> NodeList = new List<Node>();
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            Collection.Clear();
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            Debug.Assert(index < Collection.Count);
            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node NodeItem = SafeType.ItemAt<Node>(Collection, index);

            return NodeItem == childNode;
        }

        /// <summary>
        /// Gets the type of the node list at the given property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The node list type.</returns>
        public static Type ListInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = Property.PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(InterfaceType));

            return InterfaceType;
        }

        /// <summary>
        /// Gets the last index of a node list at the given property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="lastIndex">The last index.</param>
        public static void GetLastListIndex(Node node, string propertyName, out int lastIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            lastIndex = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNodeList == null) throw new ArgumentNullException(nameof(childNodeList));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));
            Debug.Assert(PropertyType.IsAssignableFrom(childNodeList.GetType()));

            Property.SetValue(node, childNodeList);
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            Debug.Assert(index <= Collection.Count);
            if (index > Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection.Insert(index, childNode);
        }

        /// <summary>
        /// Removes a node from the node list property of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="index">The index where to remove.</param>
        public static void RemoveFromList(Node node, string propertyName, int index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            Debug.Assert(index < Collection.Count);
            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection[index] = childNode;
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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = SafeType.GetPropertyValue<IList>(Property, node);

            Debug.Assert(index < Collection.Count);
            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));
            Debug.Assert(index + direction < Collection.Count);
            if (index + direction >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(direction));

            Node ChildNode = SafeType.ItemAt<Node>(Collection, index);

            Collection.RemoveAt(index);
            Collection.Insert(index + direction, ChildNode);
        }
    }
}
