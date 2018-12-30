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
        public static bool IsChildNodeProperty(INode node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
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
            Debug.Assert(childNode != null);
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

    public static class NodeTreeHelperOptional
    {
        public static bool IsOptionalChildNodeProperty(INode node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsOptionalReferenceType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);
            childNodeType = GenericArguments[0];

            return NodeTreeHelper.IsNodeInterfaceType(childNodeType);
        }

        public static bool IsOptionalChildNode(INode node, string propertyName, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsOptionalReferenceType(PropertyType))
                return false;

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);
            if (!Optional.IsAssigned)
                return false;

            INode NodeItem = Optional.Item as INode;
            Debug.Assert(NodeItem != null);

            if (NodeItem != childNode)
                return false;

            return true;
        }

        public static void GetChildNode(INode node, string propertyName, out bool isAssigned, out INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            isAssigned = false;
            childNode = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

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
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static IOptionalReference GetOptionalReference(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(Property.PropertyType));

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            return Optional;
        }

        public static void SetOptionalReference(INode node, string propertyName, IOptionalReference optional)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (optional == null) throw new ArgumentNullException(nameof(optional));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(Property.PropertyType));

            Property.SetValue(node, optional);
        }

        public static void SetOptionalChildNode(INode node, string propertyName, INode newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type ChildNodeType = newChildNode.GetType();

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsOptionalReferenceType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(ChildNodeType.GetInterface(InterfaceType.FullName) != null);

            IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
            Debug.Assert(Optional != null);

            PropertyInfo ItemProperty = Optional.GetType().GetProperty(nameof(IOptionalReference<INode>.Item));
            ItemProperty.SetValue(Optional, newChildNode);
            Optional.Assign();
        }

        public static bool IsChildNodeAssigned(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            return Optional.IsAssigned;
        }

        public static void AssignChildNode(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Assign();
        }

        public static void UnassignChildNode(INode node, string propertyName)
        {
            IOptionalReference Optional = GetOptionalReference(node, propertyName);

            Optional.Unassign();
        }
    }

    public static class NodeTreeHelperList
    {
        public static bool IsChildNodeList(INode node, string propertyName, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childNodeType = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsNodeListType(PropertyType))
                return false;

            IList Collection = Property.GetValue(node) as IList;
            if (Collection == null && Property.GetValue(node) != null)
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);
            childNodeType = GenericArguments[0];

            return NodeTreeHelper.IsNodeInterfaceType(childNodeType);
        }

        public static void GetChildNodeList(INode node, string propertyName, out IReadOnlyList<INode> childNodeList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            List<INode> NodeList = new List<INode>();

            foreach (object Item in Collection)
            {
                INode NodeItem = Item as INode;
                Debug.Assert(NodeItem != null);

                NodeList.Add(NodeItem);
            }

            childNodeList = NodeList.AsReadOnly();
        }

        public static bool IsListChildNode(INode node, string propertyName, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = Collection[index] as INode;
            Debug.Assert(NodeItem != null);

            return NodeItem == childNode;
        }

        public static Type ListInterfaceType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = Property.PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(NodeTreeHelper.IsNodeInterfaceType(InterfaceType));

            return InterfaceType;
        }

        public static bool GetLastListIndex(INode node, string propertyName, out int lastIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            lastIndex = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            lastIndex = Collection.Count;
            return true;
        }

        public static void InsertIntoList(INode node, string propertyName, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            if (index > Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection.Insert(index, childNode);
        }

        public static void RemoveFromList(INode node, string propertyName, int index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection.RemoveAt(index);
        }

        public static void ReplaceNode(INode node, string propertyName, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsNodeListType(PropertyType));

            IList Collection = Property.GetValue(node) as IList;
            Debug.Assert(Collection != null);

            if (index >= Collection.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Collection[index] = childNode;
        }
    }

    public static class NodeTreeHelperBlockList
    {
        public static bool IsChildBlockList(INode node, string propertyName, out Type childInterfaceType, out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            childInterfaceType = null;
            childNodeType = null;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsBlockListType(PropertyType))
                return false;

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            if (BlockList == null && Property.GetValue(node) != null)
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            childNodeType = GenericArguments[1];
            return true;
        }

        public static IBlockList GetBlockList(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            return BlockList;
        }

        public static void GetChildBlockList(INode node, string propertyName, out IReadOnlyList<INodeTreeBlock> childBlockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            List<INodeTreeBlock> Result = new List<INodeTreeBlock>();

            foreach (object Item in NodeBlockList)
            {
                IBlock Block = Item as IBlock;
                Debug.Assert(Block != null);

                IPattern ReplicationPattern = Block.ReplicationPattern;
                Debug.Assert(ReplicationPattern != null);
                IIdentifier SourceIdentifier = Block.SourceIdentifier;
                Debug.Assert(SourceIdentifier != null);
                IList NodeList = Block.NodeList;
                Debug.Assert(NodeList != null);
                Debug.Assert(NodeList.Count > 0);

                List<INode> ResultNodeList = new List<INode>();
                foreach (INode ChildNode in NodeList)
                    ResultNodeList.Add(ChildNode);

                Result.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ResultNodeList));
            }

            childBlockList = Result.AsReadOnly();
        }

        public static Type BlockListInterfaceType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);
            Debug.Assert(InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type BlockListItemType(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            Type ItemType = GenericArguments[1];
            Debug.Assert(ItemType != null);
            Debug.Assert(!ItemType.IsInterface);

            return ItemType;
        }

        public static bool GetLastBlockIndex(INode node, string propertyName, out int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            blockIndex = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            blockIndex = NodeBlockList.Count;
            Debug.Assert(blockIndex >= 0);

            return true;
        }

        public static bool GetLastBlockChildIndex(INode node, string propertyName, int blockIndex, out int index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            index = -1;

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            index = NodeList.Count;
            Debug.Assert(index >= 0);

            return true;
        }

        public static bool IsBlockChildNode(INode node, string propertyName, int blockIndex, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            return IsChildNode(Block, index, childNode);
        }

        public static bool IsChildNode(IBlock block, int index, INode childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = NodeList[index] as INode;
            Debug.Assert(NodeItem != null);

            if (NodeItem != childNode)
                return false;

            return true;
        }

        public static void GetChildBlock(INode node, string propertyName, int blockIndex, out IBlock childBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            childBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(childBlock != null);
        }

        public static void GetChildNode(INode node, string propertyName, int blockIndex, int index, out INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = NodeList[index] as INode;
            Debug.Assert(NodeItem != null);

            childNode = NodeItem;
        }

        public static void GetChildNode(IBlock block, int index, out INode childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            INode NodeItem = NodeList[index] as INode;
            Debug.Assert(NodeItem != null);

            childNode = NodeItem;
        }

        public static bool IsBlockPatternNode(INode node, string propertyName, int blockIndex, IPattern replicationPattern)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            return IsPatternNode(Block, replicationPattern);
        }

        public static bool IsPatternNode(IBlock block, IPattern replicationPattern)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            return replicationPattern == block.ReplicationPattern;
        }

        public static string GetPattern(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            IPattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            return NodeTreeHelper.GetText(ReplicationPattern);
        }

        public static void SetPattern(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            IPattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            NodeTreeHelper.SetText(ReplicationPattern, text);
        }

        public static bool IsBlockSourceNode(INode node, string propertyName, int blockIndex, IIdentifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            return IsSourceNode(Block, sourceIdentifier);
        }

        public static bool IsSourceNode(IBlock block, IIdentifier sourceIdentifier)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return sourceIdentifier == block.SourceIdentifier;
        }

        public static string GetSource(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            IIdentifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            return NodeTreeHelper.GetText(SourceIdentifier);
        }

        public static void SetSource(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            IIdentifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            NodeTreeHelper.SetText(SourceIdentifier, text);
        }

        public static void SetReplication(IBlock block, ReplicationStatus replication)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            block.GetType().GetProperty(nameof(IBlock.Replication)).SetValue(block, replication);
        }

        public static void InsertIntoBlock(INode node, string propertyName, int blockIndex, int index, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        public static void InsertIntoBlock(IBlock block, int index, INode childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        public static void RemoveFromBlock(INode node, string propertyName, int blockIndex, int index, out bool isBlockRemoved)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.RemoveAt(index);

            if (NodeList.Count == 0)
            {
                NodeBlockList.RemoveAt(blockIndex);
                isBlockRemoved = true;
            }
            else
                isBlockRemoved = false;
        }

        public static void ReplaceNode(INode node, string propertyName, int blockIndex, int index, INode newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        public static void ReplaceInBlock(IBlock block, int index, INode newChildNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            return (NodeBlockList.Count == 0);
        }

        public static bool IsBlockListSingle(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList.Count == 0)
                return false;

            IBlock Block = NodeBlockList[0] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);
            Debug.Assert(NodeList.Count > 0);

            return NodeList.Count == 1;
        }

        public static void InsertIntoBlockList(INode node, string propertyName, int blockIndex, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier, out IBlock childBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            childBlock = CreateBlock(Property.PropertyType, replication, replicationPattern, sourceIdentifier);
            NodeBlockList.Insert(blockIndex, childBlock);
        }

        private static IBlock CreateBlock(Type propertyType, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            Type[] TypeArguments = propertyType.GetGenericArguments();

            Type BlockType = typeof(Block<,>).MakeGenericType(TypeArguments);
            IBlock NewBlock = BlockType.Assembly.CreateInstance(BlockType.FullName) as IBlock;
            Debug.Assert(NewBlock != null);

            IDocument EmptyComment = NodeHelper.CreateEmptyDocumentation();
            BlockType.GetProperty(nameof(INode.Documentation)).SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });
            IList NewNodeList = NodeListType.Assembly.CreateInstance(NodeListType.FullName) as IList;
            Debug.Assert(NewNodeList != null);

            BlockType.GetProperty(nameof(IBlock.Replication)).SetValue(NewBlock, replication);
            BlockType.GetProperty(nameof(IBlock.NodeList)).SetValue(NewBlock, NewNodeList);
            BlockType.GetProperty(nameof(IBlock.ReplicationPattern)).SetValue(NewBlock, replicationPattern);
            BlockType.GetProperty(nameof(IBlock.SourceIdentifier)).SetValue(NewBlock, sourceIdentifier);

            return NewBlock;
        }

        public static void RemoveFromBlockList(INode node, string propertyName, int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            NodeBlockList.RemoveAt(blockIndex);
        }

        public static void SplitBlock(INode node, string propertyName, int blockIndex, int index, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier, out IBlock newChildBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock CurrentBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(CurrentBlock != null);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList != null);
            Debug.Assert(CurrentNodeList.Count > 1);

            if (index >= CurrentNodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            newChildBlock = CreateBlock(Property.PropertyType, replication, replicationPattern, sourceIdentifier);
            Debug.Assert(newChildBlock != null);

            IList NewNodeList = newChildBlock.NodeList;
            Debug.Assert(NewNodeList != null);

            NodeBlockList.Insert(blockIndex, newChildBlock);

            for (int i = 0; i < index; i++)
            {
                INode ChildNode = CurrentNodeList[0] as INode;
                Debug.Assert(ChildNode != null);

                CurrentNodeList.RemoveAt(0);
                NewNodeList.Insert(i, ChildNode);
            }

            Debug.Assert(CurrentNodeList.Count > 0);
            Debug.Assert(NewNodeList.Count > 0);
        }

        public static void MergeBlocks(INode node, string propertyName, int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex <= 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock MergedBlock = NodeBlockList[blockIndex - 1] as IBlock;
            Debug.Assert(MergedBlock != null);

            IBlock CurrentBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(CurrentBlock != null);

            IList MergedNodeList = MergedBlock.NodeList;
            Debug.Assert(MergedNodeList != null);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList != null);

            for (int i = 0; i < MergedNodeList.Count; i++)
            {
                INode ChildNode = MergedNodeList[i] as INode;
                Debug.Assert(ChildNode != null);

                CurrentNodeList.Insert(i, ChildNode);
            }

            NodeBlockList.RemoveAt(blockIndex - 1);
        }

        public static void MoveNode(IBlock block, int index, int direction)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + direction < 0 || index + direction >= NodeList.Count) throw new ArgumentException(nameof(index));

            INode Node1 = NodeList[index] as INode;
            Debug.Assert(Node1 != null);
            INode Node2 = NodeList[index + direction] as INode;
            Debug.Assert(Node2 != null);

            NodeList[index] = Node2;
            NodeList[index + direction] = Node1;
        }
    }

    public static class NodeTreeHelper
    {
        public static IList<string> EnumChildNodeProperties(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();
            Debug.Assert(Properties != null);

            List<string> Result = new List<string>();
            foreach (PropertyInfo Property in Properties)
                Result.Add(Property.Name);

            return Result;
        }

        public static bool IsNodeInterfaceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsInterface && type.GetInterface(typeof(INode).Name) != null;
        }

        public static bool IsOptionalReferenceType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IOptionalReference<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            return IsNodeInterfaceType(GenericArguments[0]);
        }

        public static bool IsNodeListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IList<>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 1);

            return IsNodeInterfaceType(GenericArguments[0]);
        }

        public static bool IsBlockListType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsInterface || !type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IBlockList<,>))
                return false;

            Type[] GenericArguments = type.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments.Length == 2);

            return IsNodeInterfaceType(GenericArguments[0]);
        }
/*
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
*/
        public static bool IsTextNode(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));

            return (Property != null);
        }

        public static string GetText(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));
            Debug.Assert(Property != null);

            string Text = Property.GetValue(node) as string;
            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetText(INode node, string text)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(IIdentifier.Text));
            Debug.Assert(Property != null);
            Debug.Assert(Property.PropertyType == typeof(string));

            Property.SetValue(node, text);
        }

        private static void GetEnumMinMax(PropertyInfo property, out int min, out int max)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            Type PropertyType = property.PropertyType;
            if (!PropertyType.IsEnum && PropertyType != typeof(bool)) throw new ArgumentException(nameof(property));

            if (PropertyType == typeof(bool))
            {
                max = 0;
                min = 1;
            }
            else
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
        }

        public static int GetEnumValue(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out int Min, out int Max);
            int Result;

            if (PropertyType == typeof(bool))
            {
                bool BoolValue = (bool)Property.GetValue(node);
                Result = BoolValue ? 1 : 0;
            }
            else
                Result = (int)Property.GetValue(node);

            Debug.Assert(Min <= Result && Result <= Max);

            return Result;
        }

        public static void SetEnumValue(INode node, string propertyName, int value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum || PropertyType == typeof(bool));

            GetEnumMinMax(Property, out int Min, out int Max);
            Debug.Assert(Min <= value && value <= Max);

            if (PropertyType == typeof(bool))
                Property.SetValue(node, value == 1 ? true : false);
            else
                Property.SetValue(node, value);
        }

        public static string GetCommentText(INode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            Debug.Assert(node.Documentation != null);
            string Text = node.Documentation.Comment;
            Debug.Assert(Text != null);

            return Text;
        }

        public static void SetCommentText(INode node, string text)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Debug.Assert(node.Documentation != null);
            node.Documentation.Comment = text;
        }

        public static void GetOptionalNodes(INode node, out IDictionary<string, IOptionalReference> optionalNodesTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            optionalNodesTable = new Dictionary<string, IOptionalReference>();

            Type NodeType = node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();
            Debug.Assert(Properties != null);

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (IsOptionalReferenceType(PropertyType))
                {
                    IOptionalReference Optional = Property.GetValue(node) as IOptionalReference;
                    Debug.Assert(Optional != null);

                    optionalNodesTable.Add(PropertyName, Optional);
                }
            }
        }

        public static void GetArgumentBlocks(INode node, out IDictionary<string, IBlockList<IArgument, Argument>> argumentBlocksTable)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            argumentBlocksTable = new Dictionary<string, IBlockList<IArgument, Argument>>();

            Type NodeType = node.GetType();
            PropertyInfo[] Properties = NodeType.GetProperties();
            Debug.Assert(Properties != null);

            foreach (PropertyInfo Property in Properties)
            {
                Type PropertyType = Property.PropertyType;
                string PropertyName = Property.Name;

                if (IsBlockListType(PropertyType))
                {
                    Debug.Assert(PropertyType.IsGenericType);
                    Type[] GenericArguments = PropertyType.GetGenericArguments();
                    Debug.Assert(GenericArguments != null);
                    Debug.Assert(GenericArguments.Length == 2);

                    if (GenericArguments[0] == typeof(IArgument))
                    {
                        IBlockList<IArgument, Argument> ArgumentBlocks = Property.GetValue(node) as IBlockList<IArgument, Argument>;
                        Debug.Assert(ArgumentBlocks != null);

                        argumentBlocksTable.Add(PropertyName, ArgumentBlocks);
                    }
                }
            }
        }

        public static bool IsDocumentProperty(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            return PropertyType == typeof(IDocument);
        }

        public static void SetDocumentation(INode node, IDocument document)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (document == null) throw new ArgumentNullException(nameof(document));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(nameof(INode.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            Property.SetValue(node, document);
        }

        public static void CopyDocumentation(INode sourceNode, INode destinationNode)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();

            if (SourceNodeType != DestinationNodeType) throw new ArgumentException();

            PropertyInfo Property = SourceNodeType.GetProperty(nameof(INode.Documentation));
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(IDocument));

            IDocument DocumentCopy = NodeHelper.CreateSimpleDocumentation(sourceNode.Documentation.Comment, sourceNode.Documentation.Uuid);
            Property.SetValue(destinationNode, DocumentCopy);
        }

        public static void CopyDocumentation(IBlock sourceBlock, IBlock destinationBlock)
        {
            if (sourceBlock == null) throw new ArgumentNullException(nameof(sourceBlock));
            if (destinationBlock == null) throw new ArgumentNullException(nameof(destinationBlock));

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
            if (sourceBlockList == null) throw new ArgumentNullException(nameof(sourceBlockList));
            if (destinationBlockList == null) throw new ArgumentNullException(nameof(destinationBlockList));

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

        private static bool IsValueProperty(INode node, string propertyName, Type type)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (PropertyType != type)
                return false;

            return true;
        }

        private static void SetValueProperty<T>(INode node, string propertyName, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (value == null) throw new ArgumentNullException(nameof(value));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(node, value);
        }

        private static void CopyValueProperty<T>(INode sourceNode, INode destinationNode, string propertyName)
        {
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType == typeof(T));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }

        public static bool IsEnumProperty(INode node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            return PropertyType.IsEnum;
        }

        public static void SetEnumProperty(INode node, string propertyName, object value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType.IsEnum);

            Property.SetValue(node, value);
        }

        public static bool IsAssignable(INode node, string propertyName, INode childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type ParentNodeType = node.GetType();
            PropertyInfo Property = ParentNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type NodeType = childNode.GetType();
            Type PropertyType = Property.PropertyType;

            Type AssignedType = null;

            if (IsNodeInterfaceType(PropertyType))
                AssignedType = PropertyType;

            else if (PropertyType.IsGenericType)
            {
                Type[] GenericArguments = PropertyType.GetGenericArguments();
                Debug.Assert(GenericArguments != null);

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
            if (sourceNode == null) throw new ArgumentNullException(nameof(sourceNode));
            if (destinationNode == null) throw new ArgumentNullException(nameof(destinationNode));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type SourceNodeType = sourceNode.GetType();
            Type DestinationNodeType = sourceNode.GetType();
            Debug.Assert(SourceNodeType == DestinationNodeType);

            PropertyInfo Property = SourceNodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            if (!PropertyType.IsEnum) throw new ArgumentException(nameof(propertyName));

            Property.SetValue(destinationNode, Property.GetValue(sourceNode));
        }
    }
}
