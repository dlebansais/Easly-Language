namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;
    using Contracts;

    /// <summary>
    /// Provides methods to manipulate nodes.
    /// </summary>
    public static partial class NodeHelper
    {
        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Argument"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Argument CreateDefaultArgument()
        {
            return CreateEmptyPositionalArgument();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="TypeArgument"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static TypeArgument CreateDefaultTypeArgument()
        {
            return CreateEmptyPositionalTypeArgument();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Body"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Body CreateDefaultBody()
        {
            return CreateEmptyEffectiveBody();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Expression"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Expression CreateDefaultExpression()
        {
            return CreateEmptyQueryExpression();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Instruction"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Instruction CreateDefaultInstruction()
        {
            return CreateEmptyCommandInstruction();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Feature"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static Feature CreateDefaultFeature()
        {
            return CreateEmptyAttributeFeature();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="ObjectType"/> with the simplest content.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static ObjectType CreateDefaultType()
        {
            return CreateEmptySimpleType();
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Node"/> with the simplest content that inherits from <paramref name="nodeType"/>.
        /// <paramref name="nodeType"/> must be one of the supported types.
        /// </summary>
        /// <param name="nodeType">The required type the new object must inherit from.</param>
        /// <returns>The created instance.</returns>
        public static Node CreateDefault(Type nodeType)
        {
            Contract.RequireNotNull(nodeType, out Type NodeType);

            if (!CreateDefaultNoCheck(NodeType, out Node Result))
                throw new ArgumentOutOfRangeException($"No default object can be created for {nodeType.FullName}");

            return Result;
        }

        /// <summary>
        /// Gets the type of the simplest object inheriting from <see cref="Node"/> that implements <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">The required interface the returned type must implement.</param>
        /// <returns>The type of the simplest object inheriting from <see cref="Node"/> that implements <paramref name="interfaceType"/>.</returns>
        public static Type GetDefaultItemType(Type interfaceType)
        {
            Type Result;

            if (interfaceType == typeof(Argument))
                Result = typeof(PositionalArgument);
            else if (interfaceType == typeof(TypeArgument))
                Result = typeof(PositionalTypeArgument);
            else if (interfaceType == typeof(Body))
                Result = typeof(EffectiveBody);
            else if (interfaceType == typeof(Expression))
                Result = typeof(QueryExpression);
            else if (interfaceType == typeof(Instruction))
                Result = typeof(CommandInstruction);
            else if (interfaceType == typeof(Feature))
                Result = typeof(AttributeFeature);
            else if (interfaceType == typeof(ObjectType))
                Result = typeof(SimpleType);
            else
                Result = interfaceType;

            Debug.Assert(Result != null, $"The returned value can't possibly be null");

            if (Result == null)
                return null!;

            return Result;
        }

        /// <summary>
        /// Creates a new instance of an object inheriting from <see cref="Node"/> with the simplest content that implements <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">The required interface the new object must implement.</param>
        /// <returns>The created instance.</returns>
        public static Node CreateDefaultFromInterface(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            if (CreateDefaultNoCheck(interfaceType, out Node Result))
                return Result;

            string NamePrefix = SafeType.AssemblyQualifiedName(interfaceType);

            NamePrefix = NamePrefix.Substring(0, NamePrefix.IndexOf(".", StringComparison.InvariantCulture) + 1);

            string NodeTypeName = SafeType.AssemblyQualifiedName(interfaceType);

            // NodeTypeName = NodeTypeName.Replace(NamePrefix + "I", NamePrefix);
            Type NodeType = SafeType.GetType(NodeTypeName);

            Debug.Assert(!NodeType.IsAbstract, $"A default type value is never abstract");

            Result = CreateEmptyNode(NodeType);
            Debug.Assert(Result != null, $"A default empty object is never null");

            if (Result == null)
                return null!;

            return Result;
        }

        /// <summary>
        /// Checks if <paramref name="type"/> is a node type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if <paramref name="type"/> is a node type; otherwise, false.</returns>
        public static bool IsNodeType(Type type)
        {
            Type? CurrentType = type;

            while (CurrentType != null && CurrentType != typeof(Node))
                CurrentType = CurrentType.BaseType;

            return CurrentType != null;
        }

        /// <summary>
        /// Creates a new instance of an object of type <paramref name="objectType"/>.
        /// </summary>
        /// <param name="objectType">The type of the new object to create.</param>
        /// <returns>The created instance.</returns>
        public static Node CreateEmptyNode(Type objectType)
        {
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            if (!IsNodeType(objectType)) throw new ArgumentException($"{nameof(objectType)} must be a node type");
            if (objectType.IsAbstract) throw new ArgumentException($"{nameof(objectType)} must not be an abstract node type");

            string FullName = SafeType.FullName(objectType);

            Node EmptyNode = SafeType.CreateInstance<Node>(objectType.Assembly, FullName);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(EmptyNode);

            foreach (string PropertyName in PropertyNames)
                CreateEmptyNodePropertyName(objectType, EmptyNode, PropertyName);

            InitializeDocumentation(EmptyNode);

            return EmptyNode;
        }

        /// <summary>
        /// Fills the content of the <paramref name="propertyName"/> property of <paramref name="emptyNode"/> with an empty content, using <paramref name="objectType"/> as the item type if the property is a collection.
        /// </summary>
        /// <param name="objectType">The item type if the property is a collection.</param>
        /// <param name="emptyNode">The node to fill.</param>
        /// <param name="propertyName">The name of the property in <paramref name="emptyNode"/> to fill.</param>
        public static void CreateEmptyNodePropertyName(Type objectType, Node emptyNode, string propertyName)
        {
            Type /*ChildInterfaceType,*/ ChildNodeType;

            if (NodeTreeHelperChild.IsChildNodeProperty(emptyNode, propertyName, out ChildNodeType))
                InitializeChildNode(emptyNode, propertyName, CreateDefaultFromInterface(ChildNodeType));
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(emptyNode, propertyName, out _))
                InitializeUnassignedOptionalChildNode(emptyNode, propertyName);
            else if (NodeTreeHelperList.IsNodeListProperty(emptyNode, propertyName, out ChildNodeType))
                CreateEmptyNodeList(objectType, emptyNode, propertyName, ChildNodeType);
            else if (NodeTreeHelperBlockList.IsBlockListProperty(emptyNode, propertyName, /*out ChildInterfaceType,*/ out ChildNodeType))
                CreateEmptyBlockList(objectType, emptyNode, propertyName, ChildNodeType);
            else if (NodeTreeHelper.IsStringProperty(emptyNode, propertyName))
                NodeTreeHelper.SetStringProperty(emptyNode, propertyName, string.Empty);
            else if (NodeTreeHelper.IsGuidProperty(emptyNode, propertyName))
                NodeTreeHelper.SetGuidProperty(emptyNode, propertyName, Guid.NewGuid());
        }

        /// <summary>
        /// Creates an empty list of <paramref name="childNodeType"/> elements in the property with name <paramref name="propertyName"/> of the <paramref name="emptyNode"/> node.
        /// </summary>
        /// <param name="objectType">Not used.</param>
        /// <param name="emptyNode">The node to fill.</param>
        /// <param name="propertyName">The name of the property in <paramref name="emptyNode"/> to fill.</param>
        /// <param name="childNodeType">The type of element elements in the created list.</param>
        public static void CreateEmptyNodeList(Type objectType, Node emptyNode, string propertyName, Type childNodeType)
        {
            if (IsCollectionNeverEmpty(emptyNode, propertyName))
            {
                // Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildNodeType);
                Type NodeType = childNodeType;

                Node FirstNode;
                if (NodeType.IsAbstract)
                    FirstNode = CreateDefault(childNodeType);
                else
                    FirstNode = CreateEmptyNode(NodeType);

                InitializeSimpleNodeList(emptyNode, propertyName, childNodeType, FirstNode);
            }
            else
                InitializeEmptyNodeList(emptyNode, propertyName, childNodeType);
        }

        /// <summary>
        /// Creates an empty block list of <paramref name="childNodeType"/> elements in the property with name <paramref name="propertyName"/> of the <paramref name="emptyNode"/> node.
        /// If the block list is not allowed to be empty, it contains an empty element upon return.
        /// </summary>
        /// <param name="objectType">Not used.</param>
        /// <param name="emptyNode">The node to fill.</param>
        /// <param name="propertyName">The name of the property in <paramref name="emptyNode"/> to fill.</param>
        /// <param name="childNodeType">The type of element elements in the created list.</param>
        public static void CreateEmptyBlockList(Type objectType, Node emptyNode, string propertyName, Type childNodeType)
        {
            if (IsCollectionNeverEmpty(emptyNode, propertyName))
            {
                // Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildInterfaceType);
                Type NodeType = childNodeType;

                Node FirstNode;
                if (NodeType.IsAbstract)
                    FirstNode = /*CreateDefault(ChildInterfaceType)*/CreateDefault(NodeType);
                else
                    FirstNode = CreateEmptyNode(NodeType);

                InitializeSimpleBlockList(emptyNode, propertyName, /*ChildInterfaceType,*/ childNodeType, FirstNode);
            }
            else
                InitializeEmptyBlockList(emptyNode, propertyName, /*ChildInterfaceType,*/ childNodeType);
        }

        /// <summary>
        /// Checks if a node is empty.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <returns>True if the node is empty; otherwise, false.</returns>
        public static bool IsEmptyNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node.GetType());

            foreach (string PropertyName in PropertyNames)
                if (!IsEmptyNodePropertyName(node, PropertyName))
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if the property of a node is empty.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is empty; otherwise, false.</returns>
        public static bool IsEmptyNodePropertyName(Node node, string propertyName)
        {
            if (NodeTreeHelperChild.IsChildNodeProperty(node, propertyName, out _))
                return IsEmptyChildNode(node, propertyName);
            else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, propertyName, out _))
                return IsEmptyOptionalChildNode(node, propertyName);
            else if (NodeTreeHelperList.IsNodeListProperty(node, propertyName, out _))
                return IsEmptyNodeList(node, propertyName);
            else if (NodeTreeHelperBlockList.IsBlockListProperty(node, propertyName, /*out ChildInterfaceType,*/ out _))
                return IsEmptyBlockList(node, propertyName);
            else if (NodeTreeHelper.IsStringProperty(node, propertyName))
                return IsEmptyStringProperty(node, propertyName);
            else if (NodeTreeHelper.IsBooleanProperty(node, propertyName) || NodeTreeHelper.IsEnumProperty(node, propertyName))
                return IsEmptyEnumProperty(node, propertyName);
            else
                return true;
        }

        /// <summary>
        /// Checks if the property of a node is an empty child node.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is an empty child node; otherwise, false.</returns>
        public static bool IsEmptyChildNode(Node node, string propertyName)
        {
            NodeTreeHelperChild.GetChildNode(node, propertyName, out Node ChildNode);
            return IsEmptyNode(ChildNode);
        }

        /// <summary>
        /// Checks if the property of a node is an unassigned optional child node.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is an unassigned optional child node; otherwise, false.</returns>
        public static bool IsEmptyOptionalChildNode(Node node, string propertyName)
        {
            NodeTreeHelperOptional.GetChildNode(node, propertyName, out bool IsAssigned, out _);
            return !IsAssigned;
        }

        /// <summary>
        /// Checks if the property of a node is an empty list of nodes.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is an empty list of nodes; otherwise, false.</returns>
        public static bool IsEmptyNodeList(Node node, string propertyName)
        {
            NodeTreeHelperList.GetChildNodeList(node, propertyName, out IReadOnlyList<Node> ChildNodeList);

            if (IsCollectionNeverEmpty(node, propertyName))
            {
                Debug.Assert(ChildNodeList.Count > 0, $"A collection that is found not empty has to have an element");

                if (ChildNodeList.Count != 1)
                    return false;

                Node ChildNode = ChildNodeList[0];
                if (!IsEmptyNode(ChildNode))
                    return false;
            }
            else if (ChildNodeList.Count > 0)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the property of a node is an empty block list.
        /// If the block list is not allowed to be empty, returns true if it contains only one default element.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is an empty block list; otherwise, false.</returns>
        public static bool IsEmptyBlockList(Node node, string propertyName)
        {
            NodeTreeHelperBlockList.GetChildBlockList(node, propertyName, out IReadOnlyList<NodeTreeBlock> ChildBlockList);

            if (IsCollectionNeverEmpty(node, propertyName))
            {
                Debug.Assert(ChildBlockList.Count > 0, $"A collection that is found not empty has to have an element");

                if (ChildBlockList.Count != 1)
                    return false;

                NodeTreeBlock FirstBlock = ChildBlockList[0];
                Debug.Assert(FirstBlock.NodeList.Count > 0, $"Blocks in block lists always have at least one node");

                if (FirstBlock.NodeList.Count != 1)
                    return false;

                Node ChildNode = FirstBlock.NodeList[0];
                if (!IsEmptyNode(ChildNode))
                    return false;
            }
            else if (ChildBlockList.Count > 0)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the property of a node is an empty string.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is an empty string; otherwise, false.</returns>
        public static bool IsEmptyStringProperty(Node node, string propertyName)
        {
            string Text = NodeTreeHelper.GetString(node, propertyName);
            Debug.Assert(Text != null, $"The content of a string property is never null");

            if (Text == null)
                return false;

            return Text.Length == 0;
        }

        /// <summary>
        /// Checks if the property of a node is an enum with default value.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>True if the node property is an enum with default value; otherwise, false.</returns>
        public static bool IsEmptyEnumProperty(Node node, string propertyName)
        {
            int Value = NodeTreeHelper.GetEnumValue(node, propertyName);
            NodeTreeHelper.GetEnumRange(node.GetType(), propertyName, out int Min, out _);

            return Value == Min;
        }

        private static bool CreateDefaultNoCheck(Type nodeType, out Node node)
        {
            if (nodeType == typeof(Body) || nodeType == typeof(EffectiveBody))
                node = CreateDefaultBody();
            else if (nodeType == typeof(Expression) || nodeType == typeof(QueryExpression))
                node = CreateDefaultExpression();
            else if (nodeType == typeof(Instruction) || nodeType == typeof(CommandInstruction))
                node = CreateDefaultInstruction();
            else if (nodeType == typeof(Feature) || nodeType == typeof(AttributeFeature))
                node = CreateDefaultFeature();
            else if (nodeType == typeof(ObjectType) || nodeType == typeof(SimpleType))
                node = CreateDefaultType();
            else
                return CreateDefaultNoCheckArgument(nodeType, out node);

            return true;
        }

        private static bool CreateDefaultNoCheckArgument(Type nodeType, out Node node)
        {
            if (nodeType == typeof(Argument) || nodeType == typeof(PositionalArgument))
                node = CreateDefaultArgument();
            else if (nodeType == typeof(TypeArgument) || nodeType == typeof(PositionalTypeArgument))
                node = CreateDefaultTypeArgument();
            else
                return CreateDefaultNoCheckSingle(nodeType, out node);

            return true;
        }

        private static bool CreateDefaultNoCheckSingle(Type nodeType, out Node node)
        {
            if (nodeType == typeof(Name))
                node = CreateEmptyName();
            else if (nodeType == typeof(Identifier))
                node = CreateEmptyIdentifier();
            else if (nodeType == typeof(QualifiedName))
                node = CreateEmptyQualifiedName();
            else if (nodeType == typeof(Scope))
                node = CreateEmptyScope();
            else if (nodeType == typeof(Import))
                node = CreateSimpleImport(string.Empty, string.Empty, ImportType.Latest);
            else
            {
                Contract.Unused(out node);
                return false;
            }

            return true;
        }
    }
}
