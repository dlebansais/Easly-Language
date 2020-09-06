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
        public static bool IsChildNodeProperty(INode node, string propertyName, out Type childNodeType)
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
            if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
                return false;

            childNodeType = PropertyType;
            return true;
        }

        public static bool IsChildNode(INode node, string propertyName, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsNodeInterfaceType(PropertyType))
                return false;

            if (Property.GetValue(node) != childNode)
                return false;

            return true;
        }

        public static void GetChildNode(INode node, string propertyName, out INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(Property.PropertyType));

            childNode = Property.GetValue(node) as INode;
        }

        public static Type ChildInterfaceType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type InterfaceType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static void SetChildNode(INode node, string propertyName, INode newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(Property.PropertyType));

            Type ChildNodeType = newChildNode.GetType();
            Debug.Assert(ChildNodeType.GetInterface(Property.PropertyType.FullName) != null);

            Property.SetValue(node, newChildNode);
        }
    }
}
