namespace BaseNodeHelper
{
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
            BlockList<TNode> Blocks = new BlockList<TNode>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = new List<IBlock<TNode>>();

            if (nodeList.Count > 0)
            {
                IBlock<TNode> Block = CreateBlock(nodeList);
                Blocks.NodeBlockList.Add(Block);
            }

            return Blocks;
        }

        /// <summary>
        /// Creates an instance of <see cref="BlockList{TNode}"/> with the specified items in it.
        /// </summary>
        /// <param name="nodeBlockList">The list of blocks of items to add to the new instance. All blocks must contain at least one element, and the created instance takes ownership of <paramref name="nodeBlockList"/>.</param>
        /// <returns>The created instance.</returns>
        internal static IBlockList<TNode> CreateBlockListFromBlockList(IList<IBlock<TNode>> nodeBlockList)
        {
            BlockList<TNode> Blocks = new BlockList<TNode>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = nodeBlockList;

            return Blocks;
        }

        /// <summary>
        /// Creates an instance of <see cref="BlockList{TNode}"/> with the specified items in it.
        /// </summary>
        /// <param name="blockList">The list of blocks to add to the created instance. The created instance makes a copy of all objects provided in <paramref name="blockList"/>.</param>
        /// <returns>The created instance.</returns>
        internal static IBlockList<TNode> CreateBlockListCopy(IBlockList<TNode> blockList)
        {
            /*if (blockList == null)
                return CreateEmptyBlockList();*/

            BlockList<TNode> Result = new BlockList<TNode>();
            Result.Documentation = NodeHelper.CreateDocumentationCopy(blockList.Documentation);
            Result.NodeBlockList = new List<IBlock<TNode>>();

            for (int BlockIndex = 0; BlockIndex < blockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<TNode> Block = blockList.NodeBlockList[BlockIndex];

                Block<TNode> NewBlock = new Block<TNode>();
                NewBlock.Documentation = NodeHelper.CreateDocumentationCopy(Block.Documentation);
                NewBlock.Replication = Block.Replication;

                Pattern NewReplicationPattern = new Pattern();
                NewReplicationPattern.Documentation = NodeHelper.CreateDocumentationCopy(Block.ReplicationPattern.Documentation);
                NewReplicationPattern.Text = Block.ReplicationPattern.Text;
                NewBlock.ReplicationPattern = NewReplicationPattern;

                Identifier NewSourceIdentifier = new Identifier();
                NewSourceIdentifier.Documentation = NodeHelper.CreateDocumentationCopy(Block.SourceIdentifier.Documentation);
                NewSourceIdentifier.Text = Block.SourceIdentifier.Text;
                NewBlock.SourceIdentifier = NewSourceIdentifier;

                List<TNode> NewNodeList = new List<TNode>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    TNode Item = Block.NodeList[Index];
                    TNode NewItem = (TNode)NodeHelper.DeepCloneNode(Item, cloneCommentGuid: false);

                    Debug.Assert(NewItem != null, $"The clone is always a {nameof(TNode)}");

                    if (NewItem != null)
                        NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                Result.NodeBlockList.Add(NewBlock);
            }

            return Result;
        }

        /// <summary>
        /// Creates an instance of <see cref="BlockList{TNode}"/>, using <paramref name="nodeList"/> as the initial list of items.
        /// </summary>
        /// <param name="nodeList">The list of items to put in the created instance.</param>
        /// <returns>The created instance.</returns>
        internal static IBlock<TNode> CreateBlock(IList<TNode> nodeList)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));
            if (nodeList.Count == 0) throw new ArgumentException($"{nameof(nodeList)} must be have at least one node");

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
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));
            if (nodeList.Count == 0) throw new ArgumentException($"{nameof(nodeList)} must be have at least one node");

            Block<TNode> Block = new Block<TNode>();
            Block.Documentation = NodeHelper.CreateEmptyDocumentation();
            Block.NodeList = nodeList;
            Block.Replication = replication;
            Block.ReplicationPattern = replicationPattern;
            Block.SourceIdentifier = sourceIdentifier;

            return Block;
        }

        /// <summary>
        /// Returns a value indicating whether the provided block list contains exactly one item.
        /// </summary>
        /// <param name="blockList">The block list.</param>
        /// <returns>True if the provided block list contains exactly one item; otheriwe, false.</returns>
        internal static bool IsSimple(IBlockList<TNode> blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            return blockList.NodeBlockList.Count == 1 && blockList.NodeBlockList[0].NodeList.Count == 1;
        }
    }
}