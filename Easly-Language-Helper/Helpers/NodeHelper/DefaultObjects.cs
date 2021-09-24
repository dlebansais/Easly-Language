#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1601 // Partial elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeHelper
    {
        public static Argument CreateDefaultArgument()
        {
            return CreateEmptyPositionalArgument();
        }

        public static TypeArgument CreateDefaultTypeArgument()
        {
            return CreateEmptyPositionalTypeArgument();
        }

        public static Body CreateDefaultBody()
        {
            return CreateEmptyEffectiveBody();
        }

        public static Expression CreateDefaultExpression()
        {
            return CreateEmptyQueryExpression();
        }

        public static Instruction CreateDefaultInstruction()
        {
            return CreateEmptyCommandInstruction();
        }

        public static Feature CreateDefaultFeature()
        {
            return CreateEmptyAttributeFeature();
        }

        public static ObjectType CreateDefaultType()
        {
            return CreateEmptySimpleType();
        }

        public static Node CreateDefault(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            Node Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
                return Result;
            else
                throw new ArgumentOutOfRangeException($"{nameof(interfaceType)}: {interfaceType.FullName}");
        }

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

        public static Node CreateDefaultFromInterface(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            Node Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
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

        public static bool IsNodeType(Type type)
        {
            Type? CurrentType = type;

            while (CurrentType != null && CurrentType != typeof(Node))
                CurrentType = CurrentType.BaseType;

            return CurrentType != null;
        }

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

        public static bool IsEmptyNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node.GetType());

            foreach (string PropertyName in PropertyNames)
                if (!IsEmptyNodePropertyName(node, PropertyName))
                    return false;

            return true;
        }

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

        public static bool IsEmptyChildNode(Node node, string propertyName)
        {
            NodeTreeHelperChild.GetChildNode(node, propertyName, out Node ChildNode);
            return IsEmptyNode(ChildNode);
        }

        public static bool IsEmptyOptionalChildNode(Node node, string propertyName)
        {
            NodeTreeHelperOptional.GetChildNode(node, propertyName, out bool IsAssigned, out _);
            return !IsAssigned;
        }

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

        public static bool IsEmptyStringProperty(Node node, string propertyName)
        {
            string Text = NodeTreeHelper.GetString(node, propertyName);
            Debug.Assert(Text != null, $"The content of a string property is never null");

            if (Text == null)
                return false;

            return Text.Length == 0;
        }

        public static bool IsEmptyEnumProperty(Node node, string propertyName)
        {
            int Value = NodeTreeHelper.GetEnumValue(node, propertyName);
            NodeTreeHelper.GetEnumRange(node.GetType(), propertyName, out int Min, out _);

            return Value == Min;
        }

        private static Node CreateDefaultNoCheck(Type interfaceType)
        {
            if (interfaceType == typeof(Body) || interfaceType == typeof(EffectiveBody))
                return CreateDefaultBody();
            else if (interfaceType == typeof(Expression) || interfaceType == typeof(QueryExpression))
                return CreateDefaultExpression();
            else if (interfaceType == typeof(Instruction) || interfaceType == typeof(CommandInstruction))
                return CreateDefaultInstruction();
            else if (interfaceType == typeof(Feature) || interfaceType == typeof(AttributeFeature))
                return CreateDefaultFeature();
            else if (interfaceType == typeof(ObjectType) || interfaceType == typeof(SimpleType))
                return CreateDefaultType();
            else
                return CreateDefaultNoCheckArgument(interfaceType);
        }

        private static Node CreateDefaultNoCheckArgument(Type interfaceType)
        {
            if (interfaceType == typeof(Argument) || interfaceType == typeof(PositionalArgument))
                return CreateDefaultArgument();
            else if (interfaceType == typeof(TypeArgument) || interfaceType == typeof(PositionalTypeArgument))
                return CreateDefaultTypeArgument();
            else
                return CreateDefaultNoCheckSingle(interfaceType);
        }

        private static Node CreateDefaultNoCheckSingle(Type interfaceType)
        {
            if (interfaceType == typeof(Name))
                return CreateEmptyName();
            else if (interfaceType == typeof(Identifier))
                return CreateEmptyIdentifier();
            else if (interfaceType == typeof(QualifiedName))
                return CreateEmptyQualifiedName();
            else if (interfaceType == typeof(Scope))
                return CreateEmptyScope();
            else if (interfaceType == typeof(Import))
                return CreateSimpleImport(string.Empty, string.Empty, ImportType.Latest);
            else
                return null!;
        }
    }
}
