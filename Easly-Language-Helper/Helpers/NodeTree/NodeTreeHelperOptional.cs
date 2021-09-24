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

    public static class NodeTreeHelperOptional
    {
        public static bool IsOptionalChildNodeProperty(Node node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            return IsOptionalChildNodeProperty(node.GetType(), propertyName, out childNodeType);
        }

        public static bool IsOptionalChildNodeProperty(Type nodeType, string propertyName, out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null!;

            if (!SafeType.CheckAndGetPropertyOf(nodeType, propertyName, out PropertyInfo Property))
                return false;

            Type PropertyType = Property.PropertyType;

            if (!NodeTreeHelper.IsOptionalReferenceType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];

            // return NodeTreeHelper.IsNodeInterfaceType(childNodeType);
            return NodeTreeHelper.IsNodeDescendantType(childNodeType);
        }

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

        public static void GetChildNode(Node node, string propertyName, out bool isAssigned, out Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            isAssigned = false;
            childNode = null!;

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);

            isAssigned = Optional.IsAssigned;

            if (Optional.HasItem)
                childNode = (Node)Optional.Item;
            else
                childNode = null!;

            Debug.Assert(!isAssigned || childNode != null);
        }

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

        public static Type OptionalChildInterfaceType(IOptionalReference optional)
        {
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            Type OptionalType = optional.GetType();

            Debug.Assert(OptionalType.IsGenericType);
            Type[] GenericArguments = OptionalType.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

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

        public static bool IsChildNodeAssigned(Node node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            return Optional.IsAssigned;
        }

        public static void AssignChildNode(Node node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Assign();
        }

        public static void UnassignChildNode(Node node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Unassign();
        }
    }
}
