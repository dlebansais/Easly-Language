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
            return Result;
        }

        public static Node CreateDefaultFromInterface(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            Node Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
                return Result;

            string NamePrefix = interfaceType.AssemblyQualifiedName;
            NamePrefix = NamePrefix.Substring(0, NamePrefix.IndexOf(".", StringComparison.InvariantCulture) + 1);

            string NodeTypeName = interfaceType.AssemblyQualifiedName;

            // NodeTypeName = NodeTypeName.Replace(NamePrefix + "I", NamePrefix);
            Type NodeType = Type.GetType(NodeTypeName);

            Debug.Assert(NodeType != null);
            Debug.Assert(!NodeType.IsAbstract, $"A default type value is never abstract");

            Result = CreateEmptyNode(NodeType);
            Debug.Assert(Result != null, $"A default empty object is never null");

            return Result;
        }

        public static bool IsNodeType(Type type)
        {
            while (type != null && type != typeof(Node))
                type = type.BaseType;

            return type != null;
        }

        public static Node CreateEmptyNode(Type objectType)
        {
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            if (!IsNodeType(objectType)) throw new ArgumentException($"{nameof(objectType)} must be a node type");
            if (objectType.IsAbstract) throw new ArgumentException($"{nameof(objectType)} must not be an abstract node type");

            Node EmptyNode = objectType.Assembly.CreateInstance(objectType.FullName) as Node;
            Debug.Assert(EmptyNode != null, $"A created instance is never null");

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(EmptyNode);

            foreach (string PropertyName in PropertyNames)
            {
                Type /*ChildInterfaceType,*/ ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(EmptyNode, PropertyName, out ChildNodeType))
                    InitializeChildNode(EmptyNode, PropertyName, CreateDefaultFromInterface(ChildNodeType));
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(EmptyNode, PropertyName, out ChildNodeType))
                    InitializeUnassignedOptionalChildNode(EmptyNode, PropertyName);
                else if (NodeTreeHelperList.IsNodeListProperty(EmptyNode, PropertyName, out ChildNodeType))
                    if (IsCollectionNeverEmpty(EmptyNode, PropertyName))
                    {
                        // Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildNodeType);
                        Type NodeType = ChildNodeType;

                        Node FirstNode;
                        if (NodeType.IsAbstract)
                            FirstNode = CreateDefault(ChildNodeType);
                        else
                            FirstNode = CreateEmptyNode(NodeType);

                        InitializeSimpleNodeList(EmptyNode, PropertyName, ChildNodeType, FirstNode);
                    }
                    else
                        InitializeEmptyNodeList(EmptyNode, PropertyName, ChildNodeType);
                else if (NodeTreeHelperBlockList.IsBlockListProperty(EmptyNode, PropertyName, /*out ChildInterfaceType,*/ out ChildNodeType))
                    if (IsCollectionNeverEmpty(EmptyNode, PropertyName))
                    {
                        // Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildInterfaceType);
                        Type NodeType = ChildNodeType;

                        Node FirstNode;
                        if (NodeType.IsAbstract)
                            FirstNode = /*CreateDefault(ChildInterfaceType)*/CreateDefault(NodeType);
                        else
                            FirstNode = CreateEmptyNode(NodeType);

                        InitializeSimpleBlockList(EmptyNode, PropertyName, /*ChildInterfaceType,*/ ChildNodeType, FirstNode);
                    }
                    else
                        InitializeEmptyBlockList(EmptyNode, PropertyName, /*ChildInterfaceType,*/ ChildNodeType);
                else if (NodeTreeHelper.IsStringProperty(EmptyNode, PropertyName))
                    NodeTreeHelper.SetStringProperty(EmptyNode, PropertyName, string.Empty);
                else if (NodeTreeHelper.IsGuidProperty(EmptyNode, PropertyName))
                    NodeTreeHelper.SetGuidProperty(EmptyNode, PropertyName, Guid.NewGuid());
            }

            InitializeDocumentation(EmptyNode);

            return EmptyNode;
        }

        public static bool IsEmptyNode(Node node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node.GetType());

            foreach (string PropertyName in PropertyNames)
            {
                Type /*ChildInterfaceType,*/ ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out Node ChildNode);
                    if (!IsEmptyNode(ChildNode))
                        return false;
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out Node ChildNode);
                    if (IsAssigned)
                        return false;
                }
                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<Node> ChildNodeList);

                    if (IsCollectionNeverEmpty(node, PropertyName))
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
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, PropertyName, /*out ChildInterfaceType,*/ out ChildNodeType))
                {
                    NodeTreeHelperBlockList.GetChildBlockList(node, PropertyName, out IReadOnlyList<NodeTreeBlock> ChildBlockList);

                    if (IsCollectionNeverEmpty(node, PropertyName))
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
                }
                else if (NodeTreeHelper.IsStringProperty(node, PropertyName))
                {
                    string Text = NodeTreeHelper.GetString(node, PropertyName);
                    Debug.Assert(Text != null, $"The content of a string property is never null");

                    if (Text.Length > 0)
                        return false;
                }
                else if (NodeTreeHelper.IsBooleanProperty(node, PropertyName) || NodeTreeHelper.IsEnumProperty(node, PropertyName))
                {
                    int Value = NodeTreeHelper.GetEnumValue(node, PropertyName);
                    NodeTreeHelper.GetEnumRange(node.GetType(), PropertyName, out int Min, out int Max);

                    if (Value != Min)
                        return false;
                }
            }

            return true;
        }

        private static Node CreateDefaultNoCheck(Type interfaceType)
        {
            Node Result;

            if (interfaceType == typeof(Argument) || interfaceType == typeof(PositionalArgument))
                Result = CreateDefaultArgument();
            else if (interfaceType == typeof(TypeArgument) || interfaceType == typeof(PositionalTypeArgument))
                Result = CreateDefaultTypeArgument();
            else if (interfaceType == typeof(Body) || interfaceType == typeof(EffectiveBody))
                Result = CreateDefaultBody();
            else if (interfaceType == typeof(Expression) || interfaceType == typeof(QueryExpression))
                Result = CreateDefaultExpression();
            else if (interfaceType == typeof(Instruction) || interfaceType == typeof(CommandInstruction))
                Result = CreateDefaultInstruction();
            else if (interfaceType == typeof(Feature) || interfaceType == typeof(AttributeFeature))
                Result = CreateDefaultFeature();
            else if (interfaceType == typeof(ObjectType) || interfaceType == typeof(SimpleType))
                Result = CreateDefaultType();
            else if (interfaceType == typeof(Name))
                Result = CreateEmptyName();
            else if (interfaceType == typeof(Identifier))
                Result = CreateEmptyIdentifier();
            else if (interfaceType == typeof(QualifiedName))
                Result = CreateEmptyQualifiedName();
            else if (interfaceType == typeof(Scope))
                Result = CreateEmptyScope();
            else if (interfaceType == typeof(Import))
                Result = CreateSimpleImport(string.Empty, string.Empty, ImportType.Latest);
            else
                Result = null;

            return Result;
        }
    }
}
