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
        public static void ClearChildBlockList(Node node, string propertyName)
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

            NodeBlockList.Clear();
        }

        public static void InsertIntoBlock(Node node, string propertyName, int blockIndex, int index, Node childNode)
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

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index <= NodeList.Count);
            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        public static void InsertIntoBlock(IBlock block, int index, Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index <= NodeList.Count);
            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        public static void RemoveFromBlock(Node node, string propertyName, int blockIndex, int index, out bool isBlockRemoved)
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

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
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

        public static void ReplaceNode(Node node, string propertyName, int blockIndex, int index, Node newChildNode)
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

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            IList NodeList = Block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        public static void ReplaceInBlock(IBlock block, int index, Node newChildNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        public static void InsertIntoBlockList(Node node, string propertyName, int blockIndex, IBlock childBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (childBlock == null) throw new ArgumentNullException(nameof(childBlock));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex <= NodeBlockList.Count);
            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            NodeBlockList.Insert(blockIndex, childBlock);
        }

        public static void RemoveFromBlockList(Node node, string propertyName, int blockIndex)
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

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            NodeBlockList.RemoveAt(blockIndex);
        }

        public static void SplitBlock(Node node, string propertyName, int blockIndex, int index, IBlock newChildBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildBlock == null) throw new ArgumentNullException(nameof(newChildBlock));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex <= NodeBlockList.Count);
            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock CurrentBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(CurrentBlock != null);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList != null);
            Debug.Assert(CurrentNodeList.Count > 1);

            Debug.Assert(index < CurrentNodeList.Count);
            if (index >= CurrentNodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            IList NewNodeList = newChildBlock.NodeList;
            Debug.Assert(NewNodeList != null);
            Debug.Assert(NewNodeList.Count == 0);

            NodeBlockList.Insert(blockIndex, newChildBlock);

            for (int i = 0; i < index; i++)
            {
                Node ChildNode = CurrentNodeList[0] as Node;
                Debug.Assert(ChildNode != null);

                CurrentNodeList.RemoveAt(0);
                NewNodeList.Insert(i, ChildNode);
            }

            Debug.Assert(CurrentNodeList.Count > 0);
            Debug.Assert(NewNodeList.Count > 0);
        }

        public static void MergeBlocks(Node node, string propertyName, int blockIndex, out IBlock mergedBlock)
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

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            mergedBlock = NodeBlockList[blockIndex - 1] as IBlock;
            Debug.Assert(mergedBlock != null);

            IBlock CurrentBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(CurrentBlock != null);

            IList MergedNodeList = mergedBlock.NodeList;
            Debug.Assert(MergedNodeList != null);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList != null);

            for (int i = 0; MergedNodeList.Count > 0; i++)
            {
                Node ChildNode = MergedNodeList[0] as Node;
                Debug.Assert(ChildNode != null);

                CurrentNodeList.Insert(i, ChildNode);
                MergedNodeList.RemoveAt(0);
            }

            NodeBlockList.RemoveAt(blockIndex - 1);
        }

        public static void MoveNode(IBlock block, int index, int direction)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            IList NodeList = block.NodeList;
            Debug.Assert(NodeList != null);

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));
            Debug.Assert(index + direction < NodeList.Count);
            if (index + direction >= NodeList.Count) throw new ArgumentException($"The sum of {nameof(index)} and {nameof(direction)} must not equal or exceed the collection count");

            Node ChildNode = NodeList[index] as Node;
            Debug.Assert(ChildNode != null);

            NodeList.RemoveAt(index);
            NodeList.Insert(index + direction, ChildNode);
        }

        public static void MoveBlock(Node node, string propertyName, int blockIndex, int direction)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (blockIndex + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            Type NodeType = node.GetType();
            PropertyInfo Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(Property.PropertyType));

            IBlockList BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            Debug.Assert(blockIndex + direction < NodeBlockList.Count);
            if (blockIndex + direction >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(direction));

            IBlock MovedBlock = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(MovedBlock != null);

            NodeBlockList.RemoveAt(blockIndex);
            NodeBlockList.Insert(blockIndex + direction, MovedBlock);
        }
    }
}
