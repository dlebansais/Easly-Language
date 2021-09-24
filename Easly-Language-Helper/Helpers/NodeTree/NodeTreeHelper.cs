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

    public static partial class NodeTreeHelper
    {
        public static IList<string> EnumChildNodeProperties(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            return EnumChildNodeProperties(NodeType);
        }

        public static IList<string> EnumChildNodeProperties(Type nodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (!IsNodeDescendantType(nodeType)) throw new ArgumentException($"{nameof(nodeType)} must inherit from {nameof(Node)}");

            if (GetBaseNodeAncestor(nodeType, out Type AncestorType))
                nodeType = AncestorType;

            IList<PropertyInfo> Properties = GetTypeProperties(nodeType);

            List<string> Result = new List<string>();
            foreach (PropertyInfo Property in Properties)
                Result.Add(Property.Name);

            Result.Sort();
            return Result;
        }

        public static PropertyInfo GetPropertyOf(Type type, string propertyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            PropertyInfo Property = SafeType.GetProperty(type, propertyName);

            foreach (Type Interface in type.GetInterfaces())
            {
                PropertyInfo? InterfaceProperty = Interface.GetProperty(propertyName);
                if (InterfaceProperty != null)
                    return InterfaceProperty;
            }

            return null!;
        }

        public static bool IsNodeInterfaceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            string FullName = SafeType.FullName(typeof(Node));

            return type.IsInterface && type.GetInterface(FullName) != null;
        }

        public static bool IsNodeDescendantType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsSubclassOf(typeof(Node));
        }

        public static Type NodeTypeToInterfaceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (type.IsInterface || type.IsAbstract) throw new ArgumentException($"{nameof(type)} must be neither an interface nor an abstract type");

            Type[] Interfaces = type.GetInterfaces();
            foreach (Type InterfaceType in Interfaces)
                if (InterfaceType.Name == $"I{type.Name}")
                    return InterfaceType;

            return null!;
        }

        public static Type InterfaceTypeToNodeType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!type.IsInterface) throw new ArgumentException($"{nameof(type)} must be an interface");

            string FullName = SafeType.FullName(type);

            string[] Prefixes = FullName.Split('.');
            Debug.Assert(Prefixes.Length > 0);

            int InterfaceIndex = 0;
            while (InterfaceIndex + 1 < Prefixes.Length && !Prefixes[InterfaceIndex].Contains("`") && !Prefixes[InterfaceIndex].Contains("["))
                InterfaceIndex++;

            string LastName = Prefixes[InterfaceIndex];
            Debug.Assert(LastName.Length > 1);
            Debug.Assert(LastName[0] == 'I');

            string NodeTypeName = LastName.Substring(1);

            string NodeTypeFullName = string.Empty;
            for (int i = 0; i < Prefixes.Length; i++)
            {
                if (i > 0)
                    NodeTypeFullName += ".";

                if (i != InterfaceIndex)
                    NodeTypeFullName += Prefixes[i];
                else
                    NodeTypeFullName += NodeTypeName;
            }

            Assembly TypeAssembly = type.Assembly;
            Type Result = SafeType.GetType(TypeAssembly, NodeTypeFullName);

            return Result;
        }

        public static bool IsOptionalReferenceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IOptionalReference<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsOptionalDescendantType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(OptionalReference<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            return IsNodeDescendantType(GenericType);
        }

        public static bool IsNodeListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsBlockListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlockList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsBlockType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlock<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            // return IsNodeInterfaceType(GenericType);
            return IsNodeDescendantType(GenericType);
        }

        public static bool IsTextNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            PropertyInfo? Property = NodeType.GetProperty(nameof(Identifier.Text));

            return Property != null;
        }

        public static bool IsAssignable(Node node, string propertyName, Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(ParentNodeType, propertyName);

            Type NodeType = childNode.GetType();
            Type PropertyType = Property.PropertyType;

            Type AssignedType = null!;

            // if (IsNodeInterfaceType(PropertyType))
            if (IsNodeDescendantType(PropertyType))
                AssignedType = PropertyType;
            else if (PropertyType.IsGenericType)
            {
                Type[] GenericArguments = PropertyType.GetGenericArguments();

                if (IsOptionalReferenceType(PropertyType))
                {
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }
                else if (IsNodeListType(PropertyType))
                {
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }
                else if (IsBlockListType(PropertyType))
                {
                    // Debug.Assert(GenericArguments.Length == 2);
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }
            }

            if (AssignedType == null)
                return false;

            if (!AssignedType.IsAssignableFrom(NodeType))
                return false;

            return true;
        }

        private static bool GetBaseNodeAncestor(Type nodeType, out Type ancestorType)
        {
            ancestorType = null!;
            bool Result = false;

            string FullName = SafeType.FullName(typeof(Node));
            string NodeFullName = SafeType.FullName(nodeType);
            string BaseNodeNamespace = FullName.Substring(0, FullName.IndexOf(".", StringComparison.InvariantCulture) + 1);

            while (nodeType != typeof(object) && !NodeFullName.StartsWith(BaseNodeNamespace, StringComparison.InvariantCulture))
            {
                nodeType = SafeType.GetBaseType(nodeType);
                NodeFullName = SafeType.FullName(nodeType);
            }

            Debug.Assert(nodeType != typeof(object));

            if (nodeType != typeof(Node) && nodeType != typeof(Node))
            {
                ancestorType = nodeType;
                Result = true;
            }

            return Result;
        }

        private static IList<PropertyInfo> GetTypeProperties(Type type)
        {
            PropertyInfo[] Properties = type.GetProperties();
            List<PropertyInfo> Result = new List<PropertyInfo>(Properties);

            foreach (Type Interface in type.GetInterfaces())
            {
                PropertyInfo[] InterfaceProperties = Interface.GetProperties();

                foreach (PropertyInfo NewProperty in InterfaceProperties)
                {
                    bool AlreadyListed = false;
                    foreach (PropertyInfo ExistingProperty in Result)
                        if (NewProperty.Name == ExistingProperty.Name)
                        {
                            AlreadyListed = true;
                            break;
                        }

                    if (!AlreadyListed)
                        Result.Add(NewProperty);
                }
            }

            return Result;
        }

        public static void GetOptionalNodes(Node node, out IDictionary<string, IOptionalReference> optionalNodesTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            optionalNodesTable = new Dictionary<string, IOptionalReference>();

            Type NodeType = node.GetType();
            IList<PropertyInfo> Properties = GetTypeProperties(NodeType);

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (IsOptionalReferenceType(PropertyType))
                {
                    IOptionalReference Optional = SafeType.GetPropertyValue<IOptionalReference>(Property, node);
                    optionalNodesTable.Add(PropertyName, Optional);
                }
            }
        }

        public static void GetArgumentBlocks(Node node, out IDictionary<string, IBlockList<Argument>> argumentBlocksTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            argumentBlocksTable = new Dictionary<string, IBlockList<Argument>>();

            Type NodeType = node.GetType();
            IList<PropertyInfo> Properties = GetTypeProperties(NodeType);

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (IsBlockListType(PropertyType))
                {
                    Debug.Assert(PropertyType.IsGenericType);
                    Type[] GenericArguments = PropertyType.GetGenericArguments();

                    // Debug.Assert(GenericArguments.Length == 2);
                    Debug.Assert(GenericArguments.Length == 1);

                    if (GenericArguments[0] == typeof(Argument))
                    {
                        IBlockList<Argument>? ArgumentBlocks = SafeType.GetPropertyValue<IBlockList<Argument>>(Property, node);

                        argumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                    }
                }
            }
        }
    }
}
