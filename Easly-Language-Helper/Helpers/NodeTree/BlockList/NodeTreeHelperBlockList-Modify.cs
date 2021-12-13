namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate block lists of nodes.
    /// </summary>
    public static partial class NodeTreeHelperBlockList
    {
        /// <summary>
        /// Clears the list of nodes in a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes to clear.</param>
        /// <param name="propertyName">The property name.</param>
        public static void ClearChildBlockList(Node node, string propertyName)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(Property.PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;
            NodeBlockList.Clear();
        }

        /// <summary>
        /// Inserts a node in the list of nodes in a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to insert.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to insert.</param>
        /// <param name="index">Index in the list of nodes where to insert.</param>
        /// <param name="childNode">The node to insert.</param>
        public static void InsertIntoBlock(Node node, string propertyName, int blockIndex, int index, Node childNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);
            IList NodeList = Block.NodeList;
            Debug.Assert(index <= NodeList.Count);
            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        /// <summary>
        /// Inserts a node in the list of nodes in a block.
        /// </summary>
        /// <param name="block">The block where to insert.</param>
        /// <param name="index">Index in the list of nodes where to insert.</param>
        /// <param name="childNode">The node to insert.</param>
        public static void InsertIntoBlock(IBlock block, int index, Node childNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (childNode == null) throw new ArgumentNullException(nameof(childNode));

            IList NodeList = block.NodeList;
            Debug.Assert(index <= NodeList.Count);
            if (index > NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList.Insert(index, childNode);
        }

        /// <summary>
        /// Removes a node in the list of nodes in a block list at the specified index.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to remove.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to remove.</param>
        /// <param name="index">Index in the list of nodes where to remove.</param>
        /// <param name="isBlockRemoved">True upon return if the block was also removed because the node was the last in that block.</param>
        public static void RemoveFromBlock(Node node, string propertyName, int blockIndex, int index, out bool isBlockRemoved)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            Type NodeType = node.GetType();

            isBlockRemoved = false;

            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);
            IList NodeList = Block.NodeList;
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

        /// <summary>
        /// Replaces a node in the list of nodes in a block list at the specified index.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to replace.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to replace.</param>
        /// <param name="index">Index in the list of nodes where to replace.</param>
        /// <param name="newChildNode">The node replacing the existing node.</param>
        public static void ReplaceNode(Node node, string propertyName, int blockIndex, int index, Node newChildNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(Property.PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);
            IList NodeList = Block.NodeList;
            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        /// <summary>
        /// Replaces a node in the list of nodes in a block list at the specified index.
        /// </summary>
        /// <param name="block">The block where to replace.</param>
        /// <param name="index">Index in the list of nodes where to replace.</param>
        /// <param name="newChildNode">The node replacing the existing node.</param>
        public static void ReplaceInBlock(IBlock block, int index, Node newChildNode)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildNode == null) throw new ArgumentNullException(nameof(newChildNode));

            IList NodeList = block.NodeList;
            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            NodeList[index] = newChildNode;
        }

        /// <summary>
        /// Inserts a block of nodes in a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to insert.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to insert.</param>
        /// <param name="childBlock">The block to insert.</param>
        public static void InsertIntoBlockList(Node node, string propertyName, int blockIndex, IBlock childBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (childBlock == null) throw new ArgumentNullException(nameof(childBlock));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(Property.PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(blockIndex <= NodeBlockList.Count);
            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            NodeBlockList.Insert(blockIndex, childBlock);
        }

        /// <summary>
        /// Removes a block of nodes from a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to remove.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to remove.</param>
        public static void RemoveFromBlockList(Node node, string propertyName, int blockIndex)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(Property.PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            NodeBlockList.RemoveAt(blockIndex);
        }

        /// <summary>
        /// Splits a block of nodes in a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to split.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to split.</param>
        /// <param name="index">Index in the list of nodes where to split.</param>
        /// <param name="newChildBlock">The new block that will contains nodes appearing before <paramref name="index"/>.</param>
        public static void SplitBlock(Node node, string propertyName, int blockIndex, int index, IBlock newChildBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (newChildBlock == null) throw new ArgumentNullException(nameof(newChildBlock));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex <= NodeBlockList.Count);
            if (blockIndex > NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock CurrentBlock = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            IList CurrentNodeList = CurrentBlock.NodeList;
            Debug.Assert(CurrentNodeList.Count > 1);

            Debug.Assert(index < CurrentNodeList.Count);
            if (index >= CurrentNodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));

            IList NewNodeList = newChildBlock.NodeList;
            Debug.Assert(NewNodeList.Count == 0);

            NodeBlockList.Insert(blockIndex, newChildBlock);

            for (int i = 0; i < index; i++)
            {
                Node ChildNode = SafeType.ItemAt<Node>(CurrentNodeList, 0);
                CurrentNodeList.RemoveAt(0);
                NewNodeList.Insert(i, ChildNode);
            }

            Debug.Assert(CurrentNodeList.Count > 0);
            Debug.Assert(NewNodeList.Count > 0);
        }

        /// <summary>
        /// Merges a block of nodes in a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to merge.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">Index of the block where to merge.</param>
        /// <param name="mergedBlock">The block that contains merged nodes upon return..</param>
        public static void MergeBlocks(Node node, string propertyName, int blockIndex, out IBlock mergedBlock)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex <= 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            Type NodeType = node.GetType();

            mergedBlock = null!;

            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);
            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock BlockFromList = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex - 1);

            mergedBlock = BlockFromList;

            IBlock CurrentBlock = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            IList MergedNodeList = mergedBlock.NodeList;
            IList CurrentNodeList = CurrentBlock.NodeList;

            for (int i = 0; MergedNodeList.Count > 0; i++)
            {
                Node ChildNode = SafeType.ItemAt<Node>(MergedNodeList, 0);
                CurrentNodeList.Insert(i, ChildNode);
                MergedNodeList.RemoveAt(0);
            }

            NodeBlockList.RemoveAt(blockIndex - 1);
        }

        /// <summary>
        /// Moves a node in a block.
        /// </summary>
        /// <param name="block">The block with the node to move.</param>
        /// <param name="index">The node idnex.</param>
        /// <param name="direction">The move direction.</param>
        public static void MoveNode(IBlock block, int index, int direction)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (index + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            IList NodeList = block.NodeList;

            Debug.Assert(index < NodeList.Count);
            if (index >= NodeList.Count) throw new ArgumentOutOfRangeException(nameof(index));
            Debug.Assert(index + direction < NodeList.Count);
            if (index + direction >= NodeList.Count) throw new ArgumentException($"The sum of {nameof(index)} and {nameof(direction)} must not equal or exceed the collection count");

            Node ChildNode = SafeType.ItemAt<Node>(NodeList, index);

            NodeList.RemoveAt(index);
            NodeList.Insert(index + direction, ChildNode);
        }

        /// <summary>
        /// Moves a block in a block list.
        /// </summary>
        /// <param name="node">The node with the property that is the block list of nodes where to move.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">The index of the block to move.</param>
        /// <param name="direction">The move direction.</param>
        public static void MoveBlock(Node node, string propertyName, int blockIndex, int direction)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (blockIndex + direction < 0) throw new ArgumentOutOfRangeException(nameof(direction));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListInterfaceType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            Debug.Assert(blockIndex + direction < NodeBlockList.Count);
            if (blockIndex + direction >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(direction));

            IBlock MovedBlock = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            NodeBlockList.RemoveAt(blockIndex);
            NodeBlockList.Insert(blockIndex + direction, MovedBlock);
        }
    }
}
