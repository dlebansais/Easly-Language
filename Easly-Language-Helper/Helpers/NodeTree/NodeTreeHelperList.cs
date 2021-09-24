﻿#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static class NodeTreeHelperList
    {
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

        public static bool GetLastListIndex(Node node, string propertyName, out int lastIndex)
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
            return true;
        }

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
