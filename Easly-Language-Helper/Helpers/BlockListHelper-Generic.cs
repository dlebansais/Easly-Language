﻿namespace BaseNodeHelper;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;

/// <summary>
/// Provides methods to manipulate block lists of nodes of the specified type.
/// </summary>
/// <typeparam name="TNode">The node type.</typeparam>
public static class BlockListHelper<TNode>
    where TNode : Node
{
    /// <summary>
    /// Creates an empty instance of <see cref="BlockList{TNode}"/>.
    /// </summary>
    /// <returns>The created instance.</returns>
    internal static IBlockList<TNode> CreateEmptyBlockList()
    {
        return CreateBlockListFromNodeList(new List<TNode>());
    }

    /// <summary>
    /// Creates an instance of <see cref="BlockList{TNode}"/> with a single item in it.
    /// </summary>
    /// <param name="node">The initial item.</param>
    /// <returns>The created instance.</returns>
    internal static IBlockList<TNode> CreateSimpleBlockList(TNode node)
    {
        IList<TNode> NodeList = new List<TNode>();
        NodeList.Add(node);

        return CreateBlockListFromNodeList(NodeList);
    }

    /// <summary>
    /// Creates an instance of <see cref="BlockList{TNode}"/> with the specified items in it.
    /// </summary>
    /// <param name="nodeList">The list of items to add to the new instance.</param>
    /// <returns>The created instance.</returns>
    internal static IBlockList<TNode> CreateBlockListFromNodeList(IList<TNode> nodeList)
    {
        Document Document = NodeHelper.CreateEmptyDocument();
        List<IBlock<TNode>> NodeBlockList = new();

        if (nodeList.Count > 0)
        {
            IBlock<TNode> Block = CreateBlock(nodeList);
            NodeBlockList.Add(Block);
        }

        BlockList<TNode> Result = new(Document, NodeBlockList);

        return Result;
    }

    /// <summary>
    /// Creates an instance of <see cref="BlockList{TNode}"/> with the specified items in it.
    /// </summary>
    /// <param name="nodeBlockList">The list of blocks of items to add to the new instance. All blocks must contain at least one element, and the created instance takes ownership of <paramref name="nodeBlockList"/>.</param>
    /// <returns>The created instance.</returns>
    internal static IBlockList<TNode> CreateBlockListFromBlockList(IList<IBlock<TNode>> nodeBlockList)
    {
        Document Document = NodeHelper.CreateEmptyDocument();
        BlockList<TNode> Result = new(Document, nodeBlockList);

        return Result;
    }

    /// <summary>
    /// Creates an instance of <see cref="BlockList{TNode}"/> with the specified items in it.
    /// </summary>
    /// <param name="blockList">The list of blocks to add to the created instance. The created instance makes a copy of all objects provided in <paramref name="blockList"/>.</param>
    /// <returns>The created instance.</returns>
    internal static IBlockList<TNode> CreateBlockListCopy(IBlockList<TNode> blockList)
    {
        /*if (blockList is null)
            return CreateEmptyBlockList();*/

        Document Document = NodeHelper.CreateEmptyDocument();
        List<IBlock<TNode>> NodeBlockList = new();

        for (int BlockIndex = 0; BlockIndex < blockList.NodeBlockList.Count; BlockIndex++)
        {
            IBlock<TNode> Block = blockList.NodeBlockList[BlockIndex];

            Document BlockDocumentation = NodeHelper.CreateDocumentationCopy(Block.Documentation);

            List<TNode> NewNodeList = new();
            for (int Index = 0; Index < Block.NodeList.Count; Index++)
            {
                TNode Item = Block.NodeList[Index];
                TNode NewItem = (TNode)NodeHelper.DeepCloneNode(Item, cloneCommentGuid: false);
                NewNodeList.Add(NewItem);
            }

            Pattern NewReplicationPattern = new(NodeHelper.CreateDocumentationCopy(Block.ReplicationPattern.Documentation), Block.ReplicationPattern.Text);
            Identifier NewSourceIdentifier = new(NodeHelper.CreateDocumentationCopy(Block.SourceIdentifier.Documentation), Block.SourceIdentifier.Text);

            Block<TNode> NewBlock = new(BlockDocumentation, NewNodeList, Block.Replication, NewReplicationPattern, NewSourceIdentifier);
            NodeBlockList.Add(NewBlock);
        }

        BlockList<TNode> Result = new(Document, NodeBlockList);

        return Result;
    }

    /// <summary>
    /// Creates an instance of <see cref="BlockList{TNode}"/>, using <paramref name="nodeList"/> as the initial list of items.
    /// </summary>
    /// <param name="nodeList">The list of items to put in the created instance.</param>
    /// <returns>The created instance.</returns>
    internal static IBlock<TNode> CreateBlock(IList<TNode> nodeList)
    {
        return CreateBlock(nodeList, ReplicationStatus.Normal, NodeHelper.CreateEmptyPattern(), NodeHelper.CreateEmptyIdentifier());
    }

    /// <summary>
    /// Creates an instance of <see cref="BlockList{TNode}"/>, using the specified parameters.
    /// </summary>
    /// <param name="nodeList">The list of items to put in the created instance.</param>
    /// <param name="replication">The replication status of the created instance.</param>
    /// <param name="replicationPattern">The replication pattern to put in the created instance.</param>
    /// <param name="sourceIdentifier">The replication source to put in the created instance.</param>
    /// <returns>The created instance.</returns>
    internal static IBlock<TNode> CreateBlock(IList<TNode> nodeList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
    {
        Document Document = NodeHelper.CreateEmptyDocument();
        Block<TNode> Result = new(Document, nodeList, replication, replicationPattern, sourceIdentifier);

        return Result;
    }

    /// <summary>
    /// Returns a value indicating whether the provided block list contains exactly one item.
    /// </summary>
    /// <param name="blockList">The block list.</param>
    /// <returns>True if the provided block list contains exactly one item; otheriwe, false.</returns>
    internal static bool IsSimple(IBlockList<TNode> blockList)
    {
        return blockList.NodeBlockList.Count == 1 && blockList.NodeBlockList[0].NodeList.Count == 1;
    }
}
