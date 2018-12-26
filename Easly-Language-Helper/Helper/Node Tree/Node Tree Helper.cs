using BaseNode;
using Easly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace BaseNodeHelper
{
    public static class NodeTreeHelperChild
    {
        public static bool IsChildNodeProperty(INode parentNode, string propertyName, out Type childNodeType)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            childNodeType = Property.PropertyType;
            if (childNodeType.GetInterface(typeof(INode).Name) == null)
                return false;

            return true;
        }

        public static bool IsChildNode(INode parentNode, string propertyName, INode node)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(node != null);

            Type ParentNodeType = parentNode.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (PropertyType.GetInterface(typeof(INode).Name) == null)
                return false;

            if (Property.GetValue(parentNode) != node)
                return false;

            return true;
        }

        public static void GetChildNode(INode node, string propertyName, out INode childNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType != null);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(INode).Name) != null);

            childNode = Property.GetValue(node) as INode;
            Debug.Assert(childNode != null);
        }

        public static Type ChildInterfaceType(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type InterfaceType = Property.PropertyType;
            Debug.Assert(!InterfaceType.IsGenericType);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static void ReplaceChildNode(INode node, string propertyName, INode childNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(childNode != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type ChildNodeType = childNode.GetType();
            Debug.Assert(ChildNodeType.GetInterface(Property.PropertyType.FullName) != null);

            Property.SetValue(node, childNode);
        }
    }

    public static class NodeTreeHelperOptional
    {
        public static bool IsOptionalChildNodeProperty(INode parentNode, string propertyName, out Type childNodeType)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            childNodeType = null;

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;

            if (PropertyType.GetGenericTypeDefinition() != typeof(OptionalReference<>))
                return false;

            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];
            if (childNodeType.GetInterface(typeof(INode).Name) == null)
                return false;

            return true;
        }

        public static bool IsOptionalChildNode(INode parentNode, string propertyName, INode node)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(node != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;

            if (PropertyType.GetGenericTypeDefinition() != typeof(OptionalReference<>))
                return false;

            IOptionalReference Optional = Property.GetValue(parentNode) as IOptionalReference;
            if (Optional == null)
                return false;
            if (!Optional.IsAssigned)
                return false;

            INode NodeItem = Optional.Item as INode;
            if (NodeItem != node)
                return false;

            return true;
        }

        public static void GetChildNode(INode node, string propertyName, out bool isAssigned, out INode childNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            isAssigned = false;
            childNode = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;

            Debug.Assert(Optional != null);

            isAssigned = Optional.IsAssigned;

            if (Optional.HasItem)
                childNode = Optional.Item as INode;
            else
                childNode = null;

            Debug.Assert(!isAssigned || childNode != null);
        }

        public static Type OptionalChildInterfaceType(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            Type[] OptionalGenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(OptionalGenericArguments != null);
            Debug.Assert(OptionalGenericArguments.Length == 1);

            Type InterfaceType = OptionalGenericArguments[0];
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static IOptionalReference GetOptionalChildNode(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            return Optional;
        }

        public static void SetOptionalChildNode(INode node, string propertyName, INode childNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(childNode != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);
            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>));

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;

            Debug.Assert(Optional != null);

            PropertyInfo ItemProperty = Optional.GetType().GetProperty(nameof(IOptionalReference<Node>.Item));
            ItemProperty.SetValue(Optional, childNode);
            Optional.Assign();
        }

        public static bool IsChildNodeAssigned(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalChildNode(node, propertyName);

            return Optional.IsAssigned;
        }

        public static void AssignChildNode(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalChildNode(node, propertyName);

            Optional.Assign();
        }

        public static void UnassignChildNode(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalChildNode(node, propertyName);

            Optional.Unassign();
        }
    }

    public static class NodeTreeHelperList
    {
        public static bool IsChildNodeList(INode node, string propertyName, out Type childNodeType)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            if (PropertyType.IsGenericType)
            {
                Type CollectionType = PropertyType.GetGenericTypeDefinition();

                if (CollectionType == typeof(IList<>))
                {
                    Type[] Generics = PropertyType.GetGenericArguments();

                    Debug.Assert(Generics.Length == 1);

                    childNodeType = Generics[0];
                    return true;
                }
            }

            childNodeType = null;
            return false;
        }

        public static bool GetChildNodeList(INode node, string propertyName, out IReadOnlyList<INode> childNodeList)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(PropertyType.IsGenericType);

            Type ListType = PropertyType.GetGenericTypeDefinition();

            Debug.Assert(ListType == typeof(IList<>) || ListType == typeof(IBlockList<,>));

            object Child = Property.GetValue(node);

            Debug.Assert(Child != null);

            if (Child is IList AsList)
            {
                List<INode> NodeList = new List<INode>();

                foreach (object Item in AsList)
                {
                    INode ChildNode = Item as INode;

                    Debug.Assert(ChildNode != null);

                    NodeList.Add(ChildNode);
                }

                childNodeList = NodeList.AsReadOnly();
                return true;
            }

            childNodeList = null;
            return false;
        }

        public static bool IsListChildNode(INode parentNode, string propertyName, int index, INode childNode)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(childNode != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;

            if (!PropertyType.IsGenericType)
                return false;

            Type ListType = PropertyType.GetGenericTypeDefinition();
            if (ListType != typeof(IList<>))
                return false;

            IList AsList = (IList)Property.GetValue(parentNode);
            if (AsList == null)
                return false;

            if (index < 0 || index >= AsList.Count)
                return false;

            if (AsList[index] != childNode)
                return false;

            return true;
        }

        public static Type ListInterfaceType(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);

            Type GenericType = Property.PropertyType.GetGenericTypeDefinition();
            Debug.Assert(GenericType == typeof(IList<>));

            Type[] GenericArguments = Property.PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static bool GetLastListIndex(INode parentNode, string propertyName, out int lastIndex)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            lastIndex = -1;

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;

            if (!PropertyType.IsGenericType)
                return false;

            Type ListType = PropertyType.GetGenericTypeDefinition();
            if (ListType != typeof(IList<>))
                return false;

            IList AsList = (IList)Property.GetValue(parentNode);
            if (AsList == null)
                return false;

            lastIndex = AsList.Count;
            return true;
        }

        public static void InsertIntoList(INode node, string propertyName, int index, INode childNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(childNode != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type GenericType = Property.PropertyType.GetGenericTypeDefinition();
            Debug.Assert(GenericType == typeof(IList<>));

            IList NodeList = Property.GetValue(node) as IList;

            Debug.Assert(NodeList != null);
            Debug.Assert(index >= 0 && index <= NodeList.Count);

            NodeList.Insert(index, childNode);
        }

        public static void RemoveFromList(INode node, string propertyName, int index)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type GenericType = Property.PropertyType.GetGenericTypeDefinition();
            Debug.Assert(GenericType == typeof(IList<>));

            IList NodeList = Property.GetValue(node) as IList;

            Debug.Assert(NodeList != null);
            Debug.Assert(index >= 0 && index < NodeList.Count);

            NodeList.RemoveAt(index);
        }
    }

    public static class NodeTreeHelperBlockList
    {
        public static bool IsChildBlockList(INode node, string propertyName, out Type childInterfaceType, out Type childNodeType)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            if (PropertyType.IsGenericType)
            {
                Type CollectionType = PropertyType.GetGenericTypeDefinition();

                if (CollectionType == typeof(IBlockList<,>))
                {
                    Type[] Generics = PropertyType.GetGenericArguments();

                    Debug.Assert(Generics.Length == 2);

                    childInterfaceType = Generics[0];
                    childNodeType = Generics[1];
                    return true;
                }
            }

            childInterfaceType = null;
            childNodeType = null;
            return false;
        }

        public static IBlockList GetBlockList(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type ListType = Property.PropertyType.GetGenericTypeDefinition();

            Debug.Assert(ListType == typeof(IBlockList<,>));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            return BlockList;
        }

        public static bool GetChildBlockList(INode node, string propertyName, out IReadOnlyList<INodeTreeBlock> childBlockList)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);

            Type ListType = Property.PropertyType.GetGenericTypeDefinition();

            Debug.Assert(ListType == typeof(IList<>) || ListType == typeof(IBlockList<,>));

            object Child = Property.GetValue(node);

            Debug.Assert(Child != null);

            if (Child is IBlockList AsBlockList)
            {
                IList NodeBlockList = AsBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(AsBlockList) as IList;

                Debug.Assert(NodeBlockList != null);

                List<INodeTreeBlock> BlockList = new List<INodeTreeBlock>();
                List<IBlock> ToRemove = new List<IBlock>();

                foreach (IBlock ChildBlock in NodeBlockList)
                {
                    List<INode> ChildNodeList = new List<INode>();

                    IPattern ReplicationPattern = (IPattern)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.ReplicationPattern)).GetValue(ChildBlock, null);
                    IIdentifier SourceIdentifier = (IIdentifier)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.SourceIdentifier)).GetValue(ChildBlock, null);
                    IList NodeList = (IList)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(ChildBlock);

                    Debug.Assert(ReplicationPattern != null);
                    Debug.Assert(SourceIdentifier != null);
                    Debug.Assert(NodeList != null);

                    if (NodeList.Count >= 0)
                    {
                        Debug.Assert(NodeList.Count > 0);

                        foreach (INode ChildNode in NodeList)
                            ChildNodeList.Add(ChildNode);

                        BlockList.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ChildNodeList));
                    }
                    else
                        ToRemove.Add(ChildBlock);
                }

                foreach (IBlock ChildBlock in ToRemove)
                    NodeBlockList.Remove(ChildBlock);

                childBlockList = BlockList.AsReadOnly();
                return true;
            }

            childBlockList = null;
            return false;
        }

        public static bool IsChildNode(IBlock childBlock, int index, INode childNode)
        {
            Debug.Assert(childBlock != null);
            Debug.Assert(childNode != null);

            IList NodeList = (IList)childBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(childBlock);
            if (NodeList == null)
                return false;

            if (index < 0 || index >= NodeList.Count)
                return false;

            if (NodeList[index] != childNode)
                return false;

            return true;
        }

        public static void GetChildNode(IBlock block, string propertyName, out INode childNode)
        {
            Debug.Assert(block != null);
            Debug.Assert(propertyName != null);

            Type NodeType = block.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            childNode = Property.GetValue(block) as INode;
            Debug.Assert(childNode != null);
        }

        public static Type BlockListInterfaceType(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            Debug.Assert(NodeBlockList != null);

            Type BlockListType = NodeBlockList.GetType();
            Debug.Assert(BlockListType.IsGenericType);

            Type[] BlockListGenericArguments = BlockListType.GetGenericArguments();
            Debug.Assert(BlockListGenericArguments != null);
            Debug.Assert(BlockListGenericArguments.Length == 1);

            Type BlockGenericArgument = BlockListGenericArguments[0];
            Debug.Assert(BlockGenericArgument.IsGenericType);

            Type[] GenericArguments = BlockGenericArgument.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type BlockListItemType(INode node, string propertyName)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsGenericType);
            Debug.Assert(PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            Debug.Assert(NodeBlockList != null);

            Type BlockListType = NodeBlockList.GetType();
            Debug.Assert(BlockListType.IsGenericType);

            Type[] BlockListGenericArguments = BlockListType.GetGenericArguments();
            Debug.Assert(BlockListGenericArguments != null);
            Debug.Assert(BlockListGenericArguments.Length == 1);

            Type BlockGenericArgument = BlockListGenericArguments[0];
            Debug.Assert(BlockGenericArgument.IsGenericType);

            Type[] GenericArguments = BlockGenericArgument.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            Type ItemType = GenericArguments[1];
            Debug.Assert(ItemType != null);
            Debug.Assert(!ItemType.IsInterface);

            return ItemType;
        }

        public static bool GetLastBlockIndex(INode parentNode, string propertyName, out int blockIndex)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            blockIndex = -1;

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(parentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            blockIndex = NodeBlockList.Count;
            return true;
        }

        public static bool GetLastBlockChildIndex(INode parentNode, string propertyName, int blockIndex, out int index)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            index = -1;

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(parentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            if (ChildBlock == null)
                return false;

            IList NodeList = (IList)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(ChildBlock);
            if (NodeList == null)
                return false;

            index = NodeList.Count;
            return true;
        }

        public static bool IsBlockChildNode(INode parentNode, string propertyName, int blockIndex, int index, INode childNode)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(childNode != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(parentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            if (ChildBlock == null)
                return false;

            return IsChildNode(ChildBlock, index, childNode);
        }

        public static void GetChildBlock(INode node, string propertyName, int blockIndex, out IBlock childBlock)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex < NodeBlockList.Count);

            childBlock = (IBlock)NodeBlockList[blockIndex];
        }

        public static bool IsBlockPatternNode(INode parentNode, string propertyName, int blockIndex, IPattern replicationPattern)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(replicationPattern != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(parentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            if (ChildBlock == null)
                return false;

            return IsPatternNode(ChildBlock, replicationPattern);
        }

        public static bool IsPatternNode(IBlock block, IPattern replicationPattern)
        {
            Debug.Assert(block != null);
            Debug.Assert(replicationPattern != null);

            Type BlockType = block.GetType();
            PropertyInfo Property = BlockType.GetProperty(nameof(IBlock<INode, Node>.ReplicationPattern));

            Debug.Assert(Property != null);
            return replicationPattern == Property.GetValue(block) as IPattern;
        }

        public static string GetPattern(IBlock block)
        {
            Debug.Assert(block != null);

            Type BlockType = block.GetType();
            PropertyInfo Property = BlockType.GetProperty(nameof(IBlock<INode, Node>.ReplicationPattern));

            Debug.Assert(Property != null);

            IPattern ReplicationPattern = Property.GetValue(block) as IPattern;

            Debug.Assert(ReplicationPattern != null);

            return NodeTreeHelper.GetText(ReplicationPattern);
        }

        public static void SetPattern(IBlock block, string text)
        {
            Debug.Assert(block != null);
            Debug.Assert(text != null);

            Type BlockType = block.GetType();
            PropertyInfo Property = BlockType.GetProperty(nameof(IBlock<INode, Node>.ReplicationPattern));

            Debug.Assert(Property != null);

            IPattern ReplicationPattern = Property.GetValue(block) as IPattern;

            Debug.Assert(ReplicationPattern != null);

            NodeTreeHelper.SetText(ReplicationPattern, text);
        }

        public static bool IsBlockSourceNode(INode parentNode, string propertyName, int blockIndex, IIdentifier sourceIdentifier)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(sourceIdentifier != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsGenericType)
                return false;
            if (PropertyType.GetInterface(typeof(IBlockList).FullName) == null)
                return false;

            IBlockList ItemBlockList = Property.GetValue(parentNode) as IBlockList;
            if (ItemBlockList == null)
                return false;

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);
            if (NodeBlockList == null)
                return false;

            if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            if (ChildBlock == null)
                return false;

            return IsSourceNode(ChildBlock, sourceIdentifier);
        }

        public static bool IsSourceNode(IBlock block, IIdentifier sourceIdentifier)
        {
            Debug.Assert(block != null);
            Debug.Assert(sourceIdentifier != null);

            Type BlockType = block.GetType();
            PropertyInfo Property = BlockType.GetProperty(nameof(IBlock<INode, Node>.SourceIdentifier));

            Debug.Assert(Property != null);
            return sourceIdentifier == Property.GetValue(block) as IIdentifier;
        }

        public static string GetSource(IBlock block)
        {
            Debug.Assert(block != null);

            Type BlockType = block.GetType();
            PropertyInfo Property = BlockType.GetProperty(nameof(IBlock<INode, Node>.SourceIdentifier));

            Debug.Assert(Property != null);

            IIdentifier SourceIdentifier = Property.GetValue(block) as IIdentifier;

            Debug.Assert(SourceIdentifier != null);

            return NodeTreeHelper.GetText(SourceIdentifier);
        }

        public static void SetSource(IBlock block, string text)
        {
            Debug.Assert(block != null);
            Debug.Assert(text != null);

            Type BlockType = block.GetType();
            PropertyInfo Property = BlockType.GetProperty(nameof(IBlock<INode, Node>.SourceIdentifier));

            Debug.Assert(Property != null);

            IIdentifier SourceIdentifier = Property.GetValue(block) as IIdentifier;

            Debug.Assert(SourceIdentifier != null);

            NodeTreeHelper.SetText(SourceIdentifier, text);
        }

        public static void SetReplication(IBlock block, ReplicationStatus replication)
        {
            block.GetType().GetProperty(nameof(IBlock<INode, Node>.Replication)).SetValue(block, replication);
        }

        public static void InsertIntoBlock(INode node, string propertyName, int blockIndex, int index, INode childNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(childNode != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex < NodeBlockList.Count);

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(ChildBlock);

            Debug.Assert(index >= 0 && index <= NodeList.Count);

            NodeList.Insert(index, childNode);
        }

        public static void InsertIntoBlock(IBlock block, int index, INode childNode)
        {
            Debug.Assert(block != null);
            IList NodeList = (IList)block.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(block);

            Debug.Assert(index >= 0 && index <= NodeList.Count);

            NodeList.Insert(index, childNode);
        }

        public static void RemoveFromBlock(INode node, string propertyName, int blockIndex, int index, out bool isBlockRemoved)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex < NodeBlockList.Count);

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(ChildBlock);

            Debug.Assert(index >= 0 && index < NodeList.Count);

            NodeList.RemoveAt(index);

            if (NodeList.Count == 0)
            {
                NodeBlockList.RemoveAt(blockIndex);
                isBlockRemoved = true;
            }
            else
                isBlockRemoved = false;
        }

        public static void ReplaceInBlock(INode node, string propertyName, int blockIndex, int index, INode newChildNode)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex < NodeBlockList.Count);

            IBlock ChildBlock = (IBlock)NodeBlockList[blockIndex];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(ChildBlock);

            Debug.Assert(index >= 0 && index < NodeList.Count);

            NodeList[index] = newChildNode;
        }

        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            Debug.Assert(blockList != null);

            IList NodeBlockList = (IList)blockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(blockList);

            Debug.Assert(NodeBlockList != null);

            return (NodeBlockList.Count == 0);
        }

        public static bool IsBlockListSingle(IBlockList blockList)
        {
            Debug.Assert(blockList != null);

            IList NodeBlockList = (IList)blockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(blockList);

            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList.Count == 0)
                return false;

            IBlock ChildBlock = (IBlock)NodeBlockList[0];
            IList NodeList = (IList)ChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(ChildBlock);

            Debug.Assert(NodeList.Count > 0);

            return NodeList.Count == 1;
        }

        public static void InsertIntoBlockList(INode node, string propertyName, int blockIndex, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier, out IBlock childBlock)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(replicationPattern != null);
            Debug.Assert(sourceIdentifier != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex <= NodeBlockList.Count);

            childBlock = CreateBlock(Property.PropertyType, replication, replicationPattern, sourceIdentifier);
            NodeBlockList.Insert(blockIndex, childBlock);
        }

        private static IBlock CreateBlock(Type propertyType, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            Type[] TypeArguments = propertyType.GetGenericArguments();

            Type BlockType = typeof(Block<,>).MakeGenericType(TypeArguments);
            IBlock NewBlock = (IBlock)BlockType.Assembly.CreateInstance(BlockType.FullName);

            Document EmptyComment = new Document();
            EmptyComment.Comment = "";
            NewBlock.GetType().GetProperty(nameof(INode.Documentation)).SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });
            IList NewNodeList = (IList)NodeListType.Assembly.CreateInstance(NodeListType.FullName);
            NewBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).SetValue(NewBlock, NewNodeList);

            NewBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.Replication)).SetValue(NewBlock, replication);
            NewBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.ReplicationPattern)).SetValue(NewBlock, replicationPattern);
            NewBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.SourceIdentifier)).SetValue(NewBlock, sourceIdentifier);

            return NewBlock;
        }

        public static void RemoveFromBlockList(INode node, string propertyName, int blockIndex)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex < NodeBlockList.Count);

            NodeBlockList.RemoveAt(blockIndex);
        }

        public static void SplitBlock(INode node, string propertyName, int blockIndex, int index, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex >= 0 && blockIndex < NodeBlockList.Count);

            IBlock CurrentBlock = (IBlock)NodeBlockList[blockIndex];
            IList CurrentNodeList = (IList)CurrentBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(CurrentBlock);

            IBlock NewChildBlock = CreateBlock(Property.PropertyType, replication, replicationPattern, sourceIdentifier);
            IList NewNodeList = (IList)NewChildBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(NewChildBlock);
            NodeBlockList.Insert(blockIndex, NewChildBlock);

            Debug.Assert(CurrentNodeList.Count > 1);
            Debug.Assert(index > 0 && index < CurrentNodeList.Count);

            for (int i = 0; i < index; i++)
            {
                INode ChildNode = (INode)CurrentNodeList[0];

                CurrentNodeList.RemoveAt(0);
                NewNodeList.Insert(i, ChildNode);
            }

            Debug.Assert(CurrentNodeList.Count > 0);
            Debug.Assert(NewNodeList.Count > 0);
        }

        public static void JoinBlocks(INode node, string propertyName, int blockIndex)
        {
            Debug.Assert(node != null);
            Debug.Assert(propertyName != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property.PropertyType.IsGenericType);
            Debug.Assert(Property.PropertyType.GetInterface(typeof(IBlockList).FullName) != null);

            IBlockList ItemBlockList = Property.GetValue(node) as IBlockList;

            Debug.Assert(ItemBlockList != null);

            IList NodeBlockList = (IList)ItemBlockList.GetType().GetProperty(nameof(IBlockList<INode, Node>.NodeBlockList)).GetValue(ItemBlockList);

            Debug.Assert(blockIndex > 0 && blockIndex < NodeBlockList.Count);

            IBlock MergedBlock = (IBlock)NodeBlockList[blockIndex - 1];
            IBlock CurrentBlock = (IBlock)NodeBlockList[blockIndex];
            IList MergedNodeList = (IList)MergedBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(MergedBlock);
            IList CurrentNodeList = (IList)CurrentBlock.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(CurrentBlock);

            for (int i = 0; i < MergedNodeList.Count; i++)
            {
                INode ChildNode = (INode)MergedNodeList[i];
                CurrentNodeList.Insert(i, ChildNode);
            }

            NodeBlockList.RemoveAt(blockIndex - 1);
        }

        public static void MoveNode(IBlock block, int index, int direction)
        {
            Debug.Assert(block != null);

            IList NodeList = (IList)block.GetType().GetProperty(nameof(IBlock<INode, Node>.NodeList)).GetValue(block);
            Debug.Assert(NodeList != null);

            Debug.Assert(index >= 0 && index < NodeList.Count);
            Debug.Assert(index + direction >= 0 && index + direction < NodeList.Count);

            INode Node1 = (INode)NodeList[index];
            INode Node2 = (INode)NodeList[index + direction];

            NodeList[index] = Node2;
            NodeList[index + direction] = Node1;
        }
    }

    public static class NodeTreeHelper
    {
        public static IList<string> EnumChildNodeProperties(INode parentNode)
        {
            Debug.Assert(parentNode != null);

            Type NodeType = parentNode.GetType();

            PropertyInfo[] Properties = NodeType.GetProperties();

            Debug.Assert(Properties != null);

            List<string> Result = new List<string>();
            foreach (PropertyInfo Property in Properties)
                Result.Add(Property.Name);

            return Result;
        }

        public static bool IsListType(Type nodeType, string propertyName)
        {
            Debug.Assert(nodeType != null);
            Debug.Assert(propertyName != null);

            PropertyInfo BlockProperty = nodeType.GetProperty(propertyName);
            if (BlockProperty == null)
                return false;

            Type CollectionType = BlockProperty.PropertyType;
            Type BlockType = CollectionType.GetGenericTypeDefinition();
            Type[] Generics = CollectionType.GetGenericArguments();

            if (Generics == null)
                return false;

            if (Generics.Length == 1)
            {
                if (!Generics[0].IsInterface || BlockType != typeof(IList<>))
                    return false;
            }

            else if (Generics.Length == 2)
            {
                if (Generics[1].IsInterface || BlockType != typeof(IBlockList<,>))
                    return false;
            }

            else
                return false;

            return true;
        }

        public static bool IsTextNode(INode node)
        {
            Debug.Assert(node != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));

            return (Property != null);
        }

        public static string GetText(INode node)
        {
            Debug.Assert(node != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));

            Debug.Assert(Property != null);

            string Text = Property.GetValue(node) as string;

            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetText(INode node, string text)
        {
            Debug.Assert(node != null);
            Debug.Assert(text != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));

            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType == typeof(string));

            Property.SetValue(node, text);
        }

        private static void GetEnumMinMax(PropertyInfo property, out int min, out int max)
        {
            Array Values = property.PropertyType.GetEnumValues();

            max = int.MinValue;
            min = int.MaxValue;
            foreach (int Value in Values)
            {
                if (max < Value)
                    max = Value;
                if (min > Value)
                    min = Value;
            }
        }

        public static int GetEnumValue(INode node, string propertyName)
        {
            Debug.Assert(node != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            int Min, Max;
            int Result;

            if (PropertyType.IsEnum)
            {
                GetEnumMinMax(Property, out Min, out Max);
                Result = (int)Property.GetValue(node);
            }
            else
            {
                Min = 0;
                Max = 1;
                bool BoolValue = (bool)Property.GetValue(node);
                Result = BoolValue ? 1 : 0;
            }

            Debug.Assert(Min <= Result && Result <= Max);

            return Result;
        }

        public static void SetEnumValue(INode node, string propertyName, int value)
        {
            Debug.Assert(node != null);

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;

            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            if (PropertyType.IsEnum)
            {
                GetEnumMinMax(Property, out int Min, out int Max);

                Debug.Assert(Min <= value && value <= Max);

                Property.SetValue(node, value);
            }
            else
            {
                Debug.Assert(value == 0 || value == 1);

                Property.SetValue(node, value == 1 ? true : false);
            }
        }

        public static string GetCommentText(INode node)
        {
            Debug.Assert(node != null);

            string Text = node.Documentation.Comment;

            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetCommentText(INode node, string text)
        {
            Debug.Assert(node != null);
            Debug.Assert(text != null);

            node.Documentation.Comment = text;
        }

        public static void GetOptionalNodes(INode node, out Dictionary<string, IOptionalReference> optionalNodesTable)
        {
            Debug.Assert(node != null);

            optionalNodesTable = new Dictionary<string, IOptionalReference>();

            Type NodeType = node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(OptionalReference<>))
                {
                    IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;

                    Debug.Assert(Optional != null);

                    optionalNodesTable.Add(PropertyName, Optional);
                }
            }
        }

        public static void GetArgumentBlocks(INode node, out Dictionary<string, IBlockList<IArgument, Argument>> argumentBlocksTable)
        {
            Debug.Assert(node != null);

            argumentBlocksTable = new Dictionary<string, IBlockList<IArgument, Argument>>();

            Type NodeType = node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(IBlockList<,>))
                {
                    Type[] GenericArguments = PropertyType.GetGenericArguments();

                    if (GenericArguments.Length > 1 && GenericArguments[0] == typeof(IArgument))
                    {
                        IBlockList<IArgument, Argument> ArgumentBlocks = Property.GetValue(node) as IBlockList<IArgument, Argument>;

                        Debug.Assert(ArgumentBlocks != null);

                        argumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                    }
                }
            }
        }

        public static bool IsDocumentProperty(INode parentNode, string propertyName)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            return PropertyType == typeof(IDocument);
        }

        public static void SetDocumentation(INode parentNode, IDocument document)
        {
            Debug.Assert(parentNode != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(INode.Documentation));

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            Property.SetValue(parentNode, document);
        }

        public static void CopyDocumentation(INode sourceNode, INode destinationNode)
        {
            Debug.Assert(sourceNode != null);
            Debug.Assert(destinationNode != null);

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(nameof(INode.Documentation));

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceNode.Documentation.Comment, sourceNode.Documentation.Uuid);
            Property.SetValue(destinationNode, DocumentCopy);
        }

        public static void CopyDocumentation(IBlock sourceBlock, IBlock destinationBlock)
        {
            Debug.Assert(sourceBlock != null);
            Debug.Assert(destinationBlock != null);

            Type SourceBlockType = sourceBlock.GetType();
            Type DestinationBlockType = sourceBlock.GetType();
            Debug.Assert(SourceBlockType == DestinationBlockType);

            PropertyInfo Property = SourceBlockType.GetProperty(nameof(IBlock.Documentation));

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlock.Documentation.Comment, sourceBlock.Documentation.Uuid);
            Property.SetValue(destinationBlock, DocumentCopy);
        }

        public static void CopyDocumentation(IBlockList sourceBlockList, IBlockList destinationBlockList)
        {
            Debug.Assert(sourceBlockList != null);
            Debug.Assert(destinationBlockList != null);

            Type SourceBlockType = sourceBlockList.GetType();
            Type DestinationBlockType = sourceBlockList.GetType();
            Debug.Assert(SourceBlockType == DestinationBlockType);

            PropertyInfo Property = SourceBlockType.GetProperty(nameof(IBlock.Documentation));

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceBlockList.Documentation.Comment, sourceBlockList.Documentation.Uuid);
            Property.SetValue(destinationBlockList, DocumentCopy);
        }

        public static bool IsBooleanProperty(INode parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(bool));
        }

        public static void SetBooleanProperty(INode parentNode, string propertyName, bool value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyBooleanProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            CopyValueProperty<bool>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsStringProperty(INode parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(string));
        }

        public static void SetStringProperty(INode parentNode, string propertyName, string value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyStringProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            CopyValueProperty<string>(sourceNode, destinationNode, propertyName);
        }

        public static bool IsGuidProperty(INode parentNode, string propertyName)
        {
            return IsValueProperty(parentNode, propertyName, typeof(Guid));
        }

        public static void SetGuidProperty(INode parentNode, string propertyName, Guid value)
        {
            SetValueProperty(parentNode, propertyName, value);
        }

        public static void CopyGuidProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            CopyValueProperty<Guid>(sourceNode, destinationNode, propertyName);
        }

        private static bool IsValueProperty(INode parentNode, string propertyName, Type type)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (PropertyType != type)
                return false;

            return true;
        }

        private static void SetValueProperty<T>(INode parentNode, string propertyName, T value)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(parentNode, value);
        }

        private static void CopyValueProperty<T>(INode sourceNode, INode destinationNode, string propertyName)
        {
            Debug.Assert(sourceNode != null);
            Debug.Assert(destinationNode != null);
            Debug.Assert(propertyName != null);

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }

        public static bool IsEnumProperty(INode parentNode, string propertyName)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            return PropertyType.IsEnum;
        }

        public static void SetEnumProperty(INode parentNode, string propertyName, object value)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);

            Type NodeType = parentNode.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum);

            Property.SetValue(parentNode, value);
        }

        public static bool IsAssignable(INode parentNode, string propertyName, INode node)
        {
            Debug.Assert(parentNode != null);
            Debug.Assert(propertyName != null);
            Debug.Assert(node != null);

            Type ParentNodeType = parentNode.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type NodeType = node.GetType();
            Type PropertyType = Property.PropertyType;

            Type AssignedType = null;

            if (PropertyType.GetInterface(typeof(INode).Name) != null)
                AssignedType = PropertyType;

            else if (PropertyType.IsGenericType)
            {
                Type GenericTypeDefinition = PropertyType.GetGenericTypeDefinition();
                Type[] GenericArguments = PropertyType.GetGenericArguments();

                if (GenericTypeDefinition == typeof(OptionalReference<>))
                {
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }

                else if (GenericTypeDefinition == typeof(IList<>))
                {
                    Debug.Assert(GenericArguments.Length == 1);
                    AssignedType = GenericArguments[0];
                }

                else if (GenericTypeDefinition == typeof(IBlockList<,>))
                {
                    Debug.Assert(GenericArguments.Length == 2);
                    AssignedType = GenericArguments[0];
                }
            }

            if (AssignedType == null)
                return false;

            if (!AssignedType.IsAssignableFrom(NodeType))
                return false;

            return true;
        }

        public static void CopyEnumProperty(INode sourceNode, INode destinationNode, string propertyName)
        {
            Debug.Assert(sourceNode != null);
            Debug.Assert(destinationNode != null);
            Debug.Assert(propertyName != null);

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);

            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum);

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }
    }
}
