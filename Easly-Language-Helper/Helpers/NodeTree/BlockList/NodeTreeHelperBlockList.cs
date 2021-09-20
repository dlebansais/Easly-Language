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

    public static partial class NodeTreeHelperBlockList
    {
        public static bool IsBlockListProperty(Node node, string propertyName, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            if (!IsBlockListProperty(NodeType, propertyName, /*out childInterfaceType,*/ out childNodeType))
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return false;

            object? Collection = Property.GetValue(node);
            Debug.Assert(Collection == null || Collection as IBlockList != null);

            return true;
        }

        public static bool IsBlockListProperty(Type nodeType, string propertyName, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            // childInterfaceType = null;
            childNodeType = null!;

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            if (!NodeTreeHelper.IsBlockListType(PropertyType))
                return false;

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            if (GenericArguments == null)
                return false;

            /*
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            Debug.Assert(childInterfaceType != null);
            Debug.Assert(childInterfaceType.IsInterface);

            childNodeType = GenericArguments[1];
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);
            */

            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];
            Debug.Assert(childNodeType != null);
            Debug.Assert(childNodeType != null && !childNodeType.IsInterface);

            return true;
        }

        public static void GetBlockType(IBlock block, /*out Type childInterfaceType,*/ out Type childNodeType)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Type BlockType = block.GetType();
            Debug.Assert(BlockType.IsGenericType);

            Type[] GenericArguments = BlockType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            childNodeType = null!;
            if (GenericArguments == null)
                return;

            /*
            Debug.Assert(GenericArguments.Length == 2);

            childInterfaceType = GenericArguments[0];
            Debug.Assert(childInterfaceType != null);
            Debug.Assert(childInterfaceType.IsInterface);

            childNodeType = GenericArguments[1];
            Debug.Assert(childNodeType != null);
            Debug.Assert(!childNodeType.IsInterface);
            */

            Debug.Assert(GenericArguments.Length == 1);

            childNodeType = GenericArguments[0];
            Debug.Assert(childNodeType != null);
            Debug.Assert(childNodeType != null && !childNodeType.IsInterface);
        }

        public static IBlockList GetBlockList(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return null!;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return null!;

            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return null!;

            return BlockList;
        }

        public static void GetChildBlockList(Node node, string propertyName, out IReadOnlyList<NodeTreeBlock> childBlockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            childBlockList = null!;
            if (NodeType == null)
                return;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return;

            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            if (BlockList == null)
            {
                childBlockList = null!;
                return;
            }
            else
            {
                IList NodeBlockList = BlockList.NodeBlockList;
                Debug.Assert(NodeBlockList != null);

                if (NodeBlockList == null)
                    return;

                List<NodeTreeBlock> Result = new List<NodeTreeBlock>();

                foreach (object? Item in NodeBlockList)
                {
                    IBlock? Block = Item as IBlock;
                    Debug.Assert(Block != null);

                    if (Block == null)
                        return;

                    Pattern ReplicationPattern = Block.ReplicationPattern;
                    Debug.Assert(ReplicationPattern != null);

                    if (ReplicationPattern == null)
                        return;

                    Identifier SourceIdentifier = Block.SourceIdentifier;
                    Debug.Assert(SourceIdentifier != null);

                    if (SourceIdentifier == null)
                        return;

                    IList NodeList = Block.NodeList;
                    Debug.Assert(NodeList != null);
                    Debug.Assert(NodeList != null && NodeList.Count > 0);

                    if (NodeList == null)
                        return;

                    List<Node> ResultNodeList = new List<Node>();
                    foreach (Node? ChildNode in NodeList)
                    {
                        if (ChildNode == null)
                            return;

                        ResultNodeList.Add(ChildNode);
                    }

                    Result.Add(new NodeTreeBlock(ReplicationPattern, SourceIdentifier, ResultNodeList));
                }

                childBlockList = Result.AsReadOnly();
            }
        }

        public static Type BlockListInterfaceType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListInterfaceType(NodeType, propertyName);
        }

        public static Type BlockListInterfaceType(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return null!;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return null!;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            if (GenericArguments == null)
                return null!;

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            Type InterfaceType = GenericArguments[0];
            Debug.Assert(InterfaceType != null);

            if (InterfaceType == null)
                return null!;

            // Debug.Assert(InterfaceType.IsInterface);
            Debug.Assert(!InterfaceType.IsInterface);

            return InterfaceType;
        }

        public static Type BlockListItemType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListItemType(NodeType, propertyName);
        }

        public static Type BlockListItemType(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return null!;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return null!;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            Debug.Assert(PropertyType.IsGenericType);
            Type[] GenericArguments = PropertyType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);

            if (GenericArguments == null)
                return null!;

            // Debug.Assert(GenericArguments.Length == 2);
            Debug.Assert(GenericArguments.Length == 1);

            // Type ItemType = GenericArguments[1];
            Type ItemType = GenericArguments[0];

            Debug.Assert(ItemType != null);
            Debug.Assert(ItemType != null && !ItemType.IsInterface);

            if (ItemType == null)
                return null!;

            return ItemType;
        }

        public static Type BlockListBlockType(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            return BlockListBlockType(NodeType, propertyName);
        }

        public static Type BlockListBlockType(Type nodeType, string propertyName)
        {
            if (nodeType == null) throw new ArgumentNullException(nameof(nodeType));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyInfo Property = NodeTreeHelper.GetPropertyOf(nodeType, propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return null!;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return null!;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            PropertyInfo? NodeListProperty = PropertyType.GetProperty(nameof(BlockList<Node>.NodeBlockList));
            Debug.Assert(NodeListProperty != null);

            if (NodeListProperty == null)
                return null!;

            Type NodeListType = NodeListProperty.PropertyType;
            Debug.Assert(NodeListType.IsGenericType);

            Type[] GenericArguments = NodeListType.GetGenericArguments();
            Debug.Assert(GenericArguments != null);
            Debug.Assert(GenericArguments != null && GenericArguments.Length == 1);

            if (GenericArguments == null)
                return null!;

            Type BlockType = GenericArguments[0];
            Debug.Assert(BlockType != null);

            if (BlockType == null)
                return null!;

            Debug.Assert(BlockType.IsInterface);

            return BlockType;
        }

        public static bool GetLastBlockIndex(Node node, string propertyName, out int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            blockIndex = -1;

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return false;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return false;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            blockIndex = NodeBlockList.Count;
            Debug.Assert(blockIndex >= 0);

            return true;
        }

        public static bool GetLastBlockChildIndex(Node node, string propertyName, int blockIndex, out int index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            index = -1;

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return false;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return false;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock? Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            if (Block == null)
                return false;

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            if (NodeList == null)
                return false;

            index = NodeList.Count;
            Debug.Assert(index >= 0);

            return true;
        }

        public static bool IsBlockChildNode(Node node, string propertyName, int blockIndex, int index, Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return false;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return false;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock? Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            if (Block == null)
                return false;

            return IsChildNode(Block, index, childNode);
        }

        public static bool IsChildNode(IBlock block, int index, Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            if (NodeList == null)
                return false;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node? NodeItem = NodeList[index] as Node;
            Debug.Assert(NodeItem != null);

            if (NodeItem == null)
                return false;

            if (NodeItem != childNode)
                return false;

            return true;
        }

        public static void GetChildBlock(Node node, string propertyName, int blockIndex, out IBlock childBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            childBlock = null!;
            if (NodeType == null)
                return;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock? BlockFromList = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(BlockFromList != null);

            if (BlockFromList == null)
                return;

            childBlock = BlockFromList;
        }

        public static void GetChildNode(Node node, string propertyName, int blockIndex, int index, out Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            childNode = null!;
            if (NodeType == null)
                return;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock? Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            if (Block == null)
                return;

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            if (NodeList == null)
                return;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node? NodeItem = NodeList[index] as Node;
            Debug.Assert(NodeItem != null);

            if (NodeItem == null)
                return;

            childNode = NodeItem;
        }

        public static void GetChildNode(IBlock block, int index, out Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            childNode = null!;
            if (NodeList == null)
                return;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            Node? NodeItem = NodeList[index] as Node;
            Debug.Assert(NodeItem != null);

            if (NodeItem == null)
                return;

            childNode = NodeItem;
        }

        public static void SetBlockList(Node node, string propertyName, IBlockList blockList)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            if (!PropertyType.IsAssignableFrom(blockList.GetType())) throw new ArgumentException($"Property {nameof(propertyName)} of {nameof(node)} must not be read-only");

            Property.SetValue(node, blockList);
        }

        public static bool IsBlockListEmpty(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            return NodeBlockList.Count == 0;
        }

        public static bool IsBlockListSingle(IBlockList blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            IList NodeBlockList = blockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            if (NodeBlockList.Count == 0)
                return false;

            IBlock? Block = NodeBlockList[0] as IBlock;
            Debug.Assert(Block != null);

            if (Block == null)
                return false;

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);
            Debug.Assert(NodeList != null && NodeList.Count > 0);

            if (NodeList == null)
                return false;

            return NodeList.Count == 1;
        }
    }
}
