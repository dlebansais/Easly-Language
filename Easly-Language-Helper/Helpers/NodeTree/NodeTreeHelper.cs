namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Contracts;
    using Easly;

    /// <summary>
    /// Provides methods to manipulate a tree of nodes.
    /// </summary>
    public static partial class NodeTreeHelper
    {
        /// <summary>
        /// Enumerates properties of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The list of property names.</returns>
        public static IList<string> EnumChildNodeProperties(Node node)
        {
            Type NodeType = node.GetType();
            return EnumChildNodeProperties(NodeType);
        }

        /// <summary>
        /// Enumerates properties of a node type.
        /// </summary>
        /// <param name="nodeType">The node type.</param>
        /// <returns>The list of property names.</returns>
        public static IList<string> EnumChildNodeProperties(Type nodeType)
        {
            if (!IsNodeDescendantType(nodeType))
                throw new ArgumentException($"{nameof(nodeType)} must inherit from {nameof(Node)}");

            if (GetBaseNodeAncestor(nodeType, out Type AncestorType))
                nodeType = AncestorType;

            IList<PropertyInfo> Properties = GetTypeProperties(nodeType);

            List<string> Result = new();
            foreach (PropertyInfo Property in Properties)
                Result.Add(Property.Name);

            Result.Sort();

            return Result;
        }

        /// <summary>
        /// Checks whether the provided type inherits from <see cref="Node"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the provided type inherits from <see cref="Node"/>; otherwise, false.</returns>
        public static bool IsNodeDescendantType(Type type)
        {
            return type.IsSubclassOf(typeof(Node));
        }

        /// <summary>
        /// Checks whether the provided type is a <see cref="IOptionalReference"/> that inherits from <see cref="Node"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the provided type is an optional reference that inherits from <see cref="Node"/>; otherwise, false.</returns>
        public static bool IsOptionalReferenceType(Type type)
        {
            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IOptionalReference<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            return IsNodeDescendantType(GenericType);
        }

        /// <summary>
        /// Checks whether the provided type is a <see cref="System.Collections.IList"/> of items that inherit from <see cref="Node"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the provided type is a <see cref="System.Collections.IList"/> of items that inherit from <see cref="Node"/>; otherwise, false.</returns>
        public static bool IsNodeListType(Type type)
        {
            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            return IsNodeDescendantType(GenericType);
        }

        /// <summary>
        /// Checks whether the provided type is a <see cref="IBlockList"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the provided type is a <see cref="IBlockList"/>; otherwise, false.</returns>
        public static bool IsBlockListType(Type type)
        {
            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlockList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            return IsNodeDescendantType(GenericType);
        }

        /// <summary>
        /// Checks whether the provided type is a <see cref="IBlock"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the provided type is a <see cref="IBlock"/>; otherwise, false.</returns>
        public static bool IsBlockType(Type type)
        {
            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlock<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments.Length == 1);

            Type GenericType = GenericArguments[0];

            return IsNodeDescendantType(GenericType);
        }

        /// <summary>
        /// Checks whether the provided node contains only text.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>True if the provided node contains only text; otherwise, false.</returns>
        public static bool IsTextNode(Node node)
        {
            Type NodeType = node.GetType();

            return SafeType.IsPropertyOf(NodeType, nameof(Identifier.Text));
        }

        /// <summary>
        /// Checks whether <paramref name="node"/> can be assigned <paramref name="childNode"/> in its property with name <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="childNode">The child node.</param>
        /// <returns>True if the node can be assigned the child node.</returns>
        public static bool IsAssignable(Node node, string propertyName, Node childNode)
        {
            Type ParentNodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(ParentNodeType, propertyName);

            Type NodeType = childNode.GetType();
            Type PropertyType = Property.PropertyType;

            Type AssignedType;

            if (IsNodeDescendantType(PropertyType))
            {
                AssignedType = PropertyType;
            }
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
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }
                else
                    return false;
            }
            else
                return false;

            if (!AssignedType.IsAssignableFrom(NodeType))
                return false;

            return true;
        }

        private static bool GetBaseNodeAncestor(Type nodeType, out Type ancestorType)
        {
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
                return true;
            }
            else
            {
                Contract.Unused(out ancestorType);
                return false;
            }
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

        /// <summary>
        /// Gets the list of optional child nodes of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="optionalNodesTable">The list of optional child nodes of a node upon return.</param>
        public static void GetOptionalNodes(Node node, out IDictionary<string, IOptionalReference> optionalNodesTable)
        {
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

        /// <summary>
        /// Gets the list of argument blocks of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="argumentBlocksTable">The list of argument blocks of a node upon return.</param>
        public static void GetArgumentBlocks(Node node, out IDictionary<string, IBlockList<Argument>> argumentBlocksTable)
        {
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
