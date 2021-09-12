#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static class NodeTreeHelperChild
    {
        public static bool IsChildNodeProperty(Node node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            return IsChildNodeProperty(node.GetType(), propertyName, out childNodeType);
        }

        public static bool IsChildNodeProperty(Type nodeType, string propertyName, out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;

            // if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
            if (!NodeTreeHelper.IsNodeDescendantType(PropertyType))
                return false;

            childNodeType = PropertyType;
            return true;
        }

        public static bool IsChildNode(Node node, string propertyName, Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
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

        public static void GetChildNode(Node node, string propertyName, out Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(Property.PropertyType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(Property.PropertyType));

            childNode = Property.GetValue(node) as Node;
        }

        public static Type ChildInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type InterfaceType = Property.PropertyType;

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(InterfaceType));

            return InterfaceType;
        }

        public static void SetChildNode(Node node, string propertyName, Node newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            // Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(Property.PropertyType));
            Debug.Assert(NodeTreeHelper.IsNodeDescendantType(Property.PropertyType));

            Type ChildNodeType = newChildNode.GetType();

            // Debug.Assert(ChildNodeType.GetInterface(Property.PropertyType.FullName) != null);
            Debug.Assert(Property.PropertyType.IsAssignableFrom(ChildNodeType));

            Property.SetValue(node, newChildNode);
        }
    }
}
