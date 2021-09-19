#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static class BlockListHelper<TNode>
        where TNode : Node
    {
        internal static IBlockList<TNode> CreateEmptyBlockList()
        {
            return CreateBlockList(new List<TNode>());
        }

        internal static IBlockList<TNode> CreateSimpleBlockList(TNode node)
        {
            IList<TNode> NodeList = new List<TNode>();
            NodeList.Add(node);

            return CreateBlockList(NodeList);
        }

        internal static IBlockList<TNode> CreateBlockList(IList<TNode> nodeList)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));

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

        internal static IBlockList<TNode> CreateBlockList(IList<IBlock<TNode>> nodeBlockList)
        {
            if (nodeBlockList == null) throw new ArgumentNullException(nameof(nodeBlockList));
            if (nodeBlockList.Count == 0) throw new ArgumentException($"{nameof(nodeBlockList)} must have at least one block");

            foreach (IBlock<TNode> Block in nodeBlockList)
                if (Block.NodeList.Count == 0) throw new ArgumentException($"All blocks in {nameof(nodeBlockList)} must have at least one node");

            BlockList<TNode> Blocks = new BlockList<TNode>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = nodeBlockList;

            return Blocks;
        }

        internal static IBlockList<TNode> CreateBlockListCopy(IBlockList<TNode> blockList)
        {
            if (blockList == null)
                return CreateEmptyBlockList();

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
                    TNode NewItem = NodeHelper.DeepCloneNode(Item, cloneCommentGuid: false) as TNode;

                    Debug.Assert(NewItem != null, $"The clone is always a {nameof(TNode)}");
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                Result.NodeBlockList.Add(NewBlock);
            }

            return Result;
        }

        internal static IBlock<TNode> CreateBlock(IList<TNode> nodeList)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));
            if (nodeList.Count == 0) throw new ArgumentException($"{nameof(nodeList)} must be have at least one node");

            return CreateBlock(nodeList, ReplicationStatus.Normal, NodeHelper.CreateEmptyPattern(), NodeHelper.CreateEmptyIdentifier());
        }

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

        internal static bool IsSimple(IBlockList<TNode> blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            return blockList.NodeBlockList.Count == 1 && blockList.NodeBlockList[0].NodeList.Count == 1;
        }
    }
}