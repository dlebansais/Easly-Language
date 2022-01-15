namespace BaseNodeHelper;

using System;
using System.Collections;
using System.Diagnostics;
using BaseNode;
using Contracts;

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        if (NodeHelper.IsCollectionNeverEmpty(Node, PropertyName))
            throw new NeverEmptyException(Node, PropertyName);

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        GetBlockNodeListInternal(Node, PropertyName, blockIndex, out _, index, isUpperIndexValid: true, out IList NodeList);

        NodeList.Insert(index, ChildNode);
    }

    /// <summary>
    /// Inserts a node in the list of nodes in a block.
    /// </summary>
    /// <param name="block">The block where to insert.</param>
    /// <param name="index">Index in the list of nodes where to insert.</param>
    /// <param name="childNode">The node to insert.</param>
    public static void InsertIntoBlock(IBlock block, int index, Node childNode)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(childNode, out Node ChildNode);

        IList NodeList = Block.NodeList;

        if (index < 0 || index > NodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        NodeList.Insert(index, ChildNode);
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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockNodeListInternal(Node, PropertyName, blockIndex, out IBlockList BlockList, index, isUpperIndexValid: false, out IList NodeList);

        if (BlockList.NodeBlockList.Count == 1 && NodeList.Count == 1 && NodeHelper.IsCollectionNeverEmpty(Node, PropertyName))
            throw new NeverEmptyException(Node, PropertyName);

        NodeList.RemoveAt(index);

        if (NodeList.Count == 0)
        {
            BlockList.NodeBlockList.RemoveAt(blockIndex);
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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(newChildNode, out Node NewChildNode);

        GetBlockNodeListInternal(Node, PropertyName, blockIndex, out _, index, isUpperIndexValid: false, out IList NodeList);

        NodeList[index] = NewChildNode;
    }

    /// <summary>
    /// Replaces a node in the list of nodes in a block list at the specified index.
    /// </summary>
    /// <param name="block">The block where to replace.</param>
    /// <param name="index">Index in the list of nodes where to replace.</param>
    /// <param name="newChildNode">The node replacing the existing node.</param>
    public static void ReplaceInBlock(IBlock block, int index, Node newChildNode)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(newChildNode, out Node NewChildNode);

        IList NodeList = Block.NodeList;

        if (index < 0 || index >= NodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        NodeList[index] = NewChildNode;
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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(childBlock, out IBlock ChildBlock);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex > NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        NodeBlockList.Insert(blockIndex, ChildBlock);
    }

    /// <summary>
    /// Removes a block of nodes from a block list.
    /// </summary>
    /// <param name="node">The node with the property that is the block list of nodes where to remove.</param>
    /// <param name="propertyName">The property name.</param>
    /// <param name="blockIndex">Index of the block where to remove.</param>
    public static void RemoveFromBlockList(Node node, string propertyName, int blockIndex)
    {
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        if (BlockList.NodeBlockList.Count == 1 && NodeHelper.IsCollectionNeverEmpty(Node, PropertyName))
            throw new NeverEmptyException(Node, PropertyName);

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(newChildBlock, out IBlock NewChildBlock);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock CurrentBlock = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

        IList CurrentNodeList = CurrentBlock.NodeList;

        if (CurrentNodeList.Count < 2)
            throw new ArgumentException($"{nameof(blockIndex)} must be the index of a block with two or more elements");

        if (index <= 0 || index >= CurrentNodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        IList NewNodeList = NewChildBlock.NodeList;

        if (NewNodeList.Count > 0)
            throw new ArgumentException($"{nameof(newChildBlock)} must be empty");

        NodeBlockList.Insert(blockIndex, NewChildBlock);

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex <= 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

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
        Contract.RequireNotNull(block, out IBlock Block);

        IList NodeList = Block.NodeList;

        if (index < 0 || index >= NodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (index + direction < 0 || index + direction >= NodeList.Count)
            throw new ArgumentOutOfRangeException(nameof(direction));

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);

        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));
        if (blockIndex + direction < 0 || blockIndex + direction >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(direction));

        IBlock MovedBlock = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

        NodeBlockList.RemoveAt(blockIndex);
        NodeBlockList.Insert(blockIndex + direction, MovedBlock);
    }

    private static void GetBlockNodeListInternal(Node node, string propertyName, int blockIndex, out IBlockList blockList, int index, bool isUpperIndexValid, out IList nodeList)
    {
        GetBlockListInternal(node, propertyName, out _, out _, out blockList);

        IList NodeBlockList = blockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);
        nodeList = Block.NodeList;

        if (index < 0 || (!isUpperIndexValid && index >= nodeList.Count) || (isUpperIndexValid && index > nodeList.Count))
            throw new ArgumentOutOfRangeException(nameof(index));
    }
}
