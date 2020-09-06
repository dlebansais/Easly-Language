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
        public static IArgument CreateDefaultArgument()
        {
            return CreateEmptyPositionalArgument();
        }

        public static ITypeArgument CreateDefaultTypeArgument()
        {
            return CreateEmptyPositionalTypeArgument();
        }

        public static IBody CreateDefaultBody()
        {
            return CreateEmptyEffectiveBody();
        }

        public static IExpression CreateDefaultExpression()
        {
            return CreateEmptyQueryExpression();
        }

        public static IInstruction CreateDefaultInstruction()
        {
            return CreateEmptyCommandInstruction();
        }

        public static IFeature CreateDefaultFeature()
        {
            return CreateEmptyAttributeFeature();
        }

        public static IObjectType CreateDefaultType()
        {
            return CreateEmptySimpleType();
        }

        public static INode CreateDefault(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            INode Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
                return Result;
            else
                throw new ArgumentOutOfRangeException($"{nameof(interfaceType)}: {interfaceType.FullName}");
        }

        public static Type GetDefaultItemType(Type interfaceType)
        {
            Type Result;

            if (interfaceType == typeof(IArgument))
                Result = typeof(IPositionalArgument);
            else if (interfaceType == typeof(ITypeArgument))
                Result = typeof(IPositionalTypeArgument);
            else if (interfaceType == typeof(IBody))
                Result = typeof(IEffectiveBody);
            else if (interfaceType == typeof(IExpression))
                Result = typeof(IQueryExpression);
            else if (interfaceType == typeof(IInstruction))
                Result = typeof(ICommandInstruction);
            else if (interfaceType == typeof(IFeature))
                Result = typeof(IAttributeFeature);
            else if (interfaceType == typeof(IObjectType))
                Result = typeof(ISimpleType);
            else
                Result = interfaceType;

            Debug.Assert(Result != null);
            return Result;
        }

        public static INode CreateDefaultFromInterface(Type interfaceType)
        {
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            INode Result = CreateDefaultNoCheck(interfaceType);

            if (Result != null)
                return Result;

            string NamePrefix = interfaceType.AssemblyQualifiedName;
            NamePrefix = NamePrefix.Substring(0, NamePrefix.IndexOf(".", StringComparison.InvariantCulture) + 1);

            string NodeTypeName = interfaceType.AssemblyQualifiedName;
            NodeTypeName = NodeTypeName.Replace(NamePrefix + "I", NamePrefix);

            Type NodeType = Type.GetType(NodeTypeName);
            Debug.Assert(!NodeType.IsAbstract);

            Result = CreateEmptyNode(NodeType);
            Debug.Assert(Result != null);

            return Result;
        }

        public static bool IsNodeType(Type type)
        {
            while (type != null && type != typeof(Node))
                type = type.BaseType;

            return type != null;
        }

        public static INode CreateEmptyNode(Type objectType)
        {
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));

            Debug.Assert(IsNodeType(objectType));
            Debug.Assert(!objectType.IsAbstract);

            INode EmptyNode = objectType.Assembly.CreateInstance(objectType.FullName) as INode;
            Debug.Assert(EmptyNode != null);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(EmptyNode);

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(EmptyNode, PropertyName, out ChildNodeType))
                    InitializeChildNode(EmptyNode, PropertyName, CreateDefaultFromInterface(ChildNodeType));
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(EmptyNode, PropertyName, out ChildNodeType))
                    InitializeUnassignedOptionalChildNode(EmptyNode, PropertyName);
                else if (NodeTreeHelperList.IsNodeListProperty(EmptyNode, PropertyName, out ChildNodeType))
                    if (IsCollectionNeverEmpty(EmptyNode, PropertyName))
                    {
                        Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildNodeType);

                        INode FirstNode;
                        if (NodeType.IsAbstract)
                            FirstNode = CreateDefault(ChildNodeType);
                        else
                            FirstNode = CreateEmptyNode(NodeType);

                        InitializeSimpleNodeList(EmptyNode, PropertyName, ChildNodeType, FirstNode);
                    }
                    else
                        InitializeEmptyNodeList(EmptyNode, PropertyName, ChildNodeType);
                else if (NodeTreeHelperBlockList.IsBlockListProperty(EmptyNode, PropertyName, out ChildInterfaceType, out ChildNodeType))
                    if (IsCollectionNeverEmpty(EmptyNode, PropertyName))
                    {
                        Type NodeType = NodeTreeHelper.InterfaceTypeToNodeType(ChildInterfaceType);

                        INode FirstNode;
                        if (NodeType.IsAbstract)
                            FirstNode = CreateDefault(ChildInterfaceType);
                        else
                            FirstNode = CreateEmptyNode(NodeType);

                        InitializeSimpleBlockList(EmptyNode, PropertyName, ChildInterfaceType, ChildNodeType, FirstNode);
                    }
                    else
                        InitializeEmptyBlockList(EmptyNode, PropertyName, ChildInterfaceType, ChildNodeType);
                else if (NodeTreeHelper.IsStringProperty(EmptyNode, PropertyName))
                    NodeTreeHelper.SetStringProperty(EmptyNode, PropertyName, string.Empty);
                else if (NodeTreeHelper.IsGuidProperty(EmptyNode, PropertyName))
                    NodeTreeHelper.SetGuidProperty(EmptyNode, PropertyName, Guid.NewGuid());
            }

            InitializeDocumentation(EmptyNode);

            return EmptyNode;
        }

        public static bool IsEmptyNode(INode node)
        {
            Debug.Assert(node != null);

            IList<string> PropertyNames = NodeTreeHelper.EnumChildNodeProperties(node.GetType());

            foreach (string PropertyName in PropertyNames)
            {
                Type ChildInterfaceType, ChildNodeType;

                if (NodeTreeHelperChild.IsChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperChild.GetChildNode(node, PropertyName, out INode ChildNode);
                    if (!IsEmptyNode(ChildNode))
                        return false;
                }
                else if (NodeTreeHelperOptional.IsOptionalChildNodeProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperOptional.GetChildNode(node, PropertyName, out bool IsAssigned, out INode ChildNode);
                    if (IsAssigned)
                        return false;
                }
                else if (NodeTreeHelperList.IsNodeListProperty(node, PropertyName, out ChildNodeType))
                {
                    NodeTreeHelperList.GetChildNodeList(node, PropertyName, out IReadOnlyList<INode> ChildNodeList);

                    if (IsCollectionNeverEmpty(node, PropertyName))
                    {
                        Debug.Assert(ChildNodeList.Count > 0);

                        if (ChildNodeList.Count != 1)
                            return false;

                        INode ChildNode = ChildNodeList[0];
                        if (!IsEmptyNode(ChildNode))
                            return false;
                    }
                    else if (ChildNodeList.Count > 0)
                        return false;
                }
                else if (NodeTreeHelperBlockList.IsBlockListProperty(node, PropertyName, out ChildInterfaceType, out ChildNodeType))
                {
                    NodeTreeHelperBlockList.GetChildBlockList(node, PropertyName, out IReadOnlyList<INodeTreeBlock> ChildBlockList);

                    if (IsCollectionNeverEmpty(node, PropertyName))
                    {
                        Debug.Assert(ChildBlockList.Count > 0);

                        if (ChildBlockList.Count != 1)
                            return false;

                        INodeTreeBlock FirstBlock = ChildBlockList[0];
                        Debug.Assert(FirstBlock.NodeList.Count > 0);

                        if (FirstBlock.NodeList.Count != 1)
                            return false;

                        INode ChildNode = FirstBlock.NodeList[0];
                        if (!IsEmptyNode(ChildNode))
                            return false;
                    }
                    else if (ChildBlockList.Count > 0)
                        return false;
                }
                else if (NodeTreeHelper.IsStringProperty(node, PropertyName))
                {
                    string Text = NodeTreeHelper.GetString(node, PropertyName);
                    Debug.Assert(Text != null);

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

        private static INode CreateDefaultNoCheck(Type interfaceType)
        {
            INode Result;

            if (interfaceType == typeof(IArgument) || interfaceType == typeof(IPositionalArgument))
                Result = CreateDefaultArgument();
            else if (interfaceType == typeof(ITypeArgument) || interfaceType == typeof(IPositionalTypeArgument))
                Result = CreateDefaultTypeArgument();
            else if (interfaceType == typeof(IBody) || interfaceType == typeof(IEffectiveBody))
                Result = CreateDefaultBody();
            else if (interfaceType == typeof(IExpression) || interfaceType == typeof(IQueryExpression))
                Result = CreateDefaultExpression();
            else if (interfaceType == typeof(IInstruction) || interfaceType == typeof(ICommandInstruction))
                Result = CreateDefaultInstruction();
            else if (interfaceType == typeof(IFeature) || interfaceType == typeof(IAttributeFeature))
                Result = CreateDefaultFeature();
            else if (interfaceType == typeof(IObjectType) || interfaceType == typeof(ISimpleType))
                Result = CreateDefaultType();
            else if (interfaceType == typeof(IName))
                Result = CreateEmptyName();
            else if (interfaceType == typeof(IIdentifier))
                Result = CreateEmptyIdentifier();
            else if (interfaceType == typeof(IQualifiedName))
                Result = CreateEmptyQualifiedName();
            else if (interfaceType == typeof(IScope))
                Result = CreateEmptyScope();
            else if (interfaceType == typeof(IImport))
                Result = CreateSimpleImport(string.Empty, string.Empty, ImportType.Latest);
            else
                Result = null;

            return Result;
        }
    }
}
