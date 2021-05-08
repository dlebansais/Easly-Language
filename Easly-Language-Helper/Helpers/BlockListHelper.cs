namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static class BlockListHelper
    {
        public static IBlockList<TNodeInterface, TNode> CreateEmptyBlockList<TNodeInterface, TNode>()
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateEmptyBlockList();
        }

        public static IBlockList<TNodeInterface, TNode> CreateSimpleBlockList<TNodeInterface, TNode>(TNodeInterface node)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateSimpleBlockList(node);
        }

        public static IBlockList<TNodeInterface, TNode> CreateBlockList<TNodeInterface, TNode>(IList<TNodeInterface> nodeList)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateBlockList(nodeList);
        }

        public static IBlockList<TNodeInterface, TNode> CreateBlockList<TNodeInterface, TNode>(IList<IBlock<TNodeInterface, TNode>> nodeBlockList)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateBlockList(nodeBlockList);
        }

        public static IBlockList<TNodeInterface, TNode> CreateBlockListCopy<TNodeInterface, TNode>(IBlockList<TNodeInterface, TNode> blockList)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateBlockListCopy(blockList);
        }

        public static IBlock<TNodeInterface, TNode> CreateBlock<TNodeInterface, TNode>(IList<TNodeInterface> nodeList)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateBlock(nodeList);
        }

        public static IBlock<TNodeInterface, TNode> CreateBlock<TNodeInterface, TNode>(IList<TNodeInterface> nodeList, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.CreateBlock(nodeList, replication, replicationPattern, sourceIdentifier);
        }

        public static bool IsSimple<TNodeInterface, TNode>(IBlockList<TNodeInterface, TNode> blockList)
            where TNodeInterface : class, INode
            where TNode : Node, TNodeInterface
        {
            return BlockListHelper<TNodeInterface, TNode>.IsSimple(blockList);
        }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public static class BlockListHelper<TNodeInterface, TNode>
#pragma warning restore SA1402 // File may only contain a single type
        where TNodeInterface : class, INode
        where TNode : Node, TNodeInterface
    {
        internal static IBlockList<TNodeInterface, TNode> CreateEmptyBlockList()
        {
            return CreateBlockList(new List<TNodeInterface>());
        }

        internal static IBlockList<TNodeInterface, TNode> CreateSimpleBlockList(TNodeInterface node)
        {
            List<TNodeInterface> NodeList = new List<TNodeInterface>();
            NodeList.Add(node);

            return CreateBlockList(NodeList);
        }

        internal static IBlockList<TNodeInterface, TNode> CreateBlockList(IList<TNodeInterface> nodeList)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));

            BlockList<TNodeInterface, TNode> Blocks = new BlockList<TNodeInterface, TNode>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = new List<IBlock<TNodeInterface, TNode>>();

            if (nodeList.Count > 0)
            {
                IBlock<TNodeInterface, TNode> Block = CreateBlock(nodeList);
                Blocks.NodeBlockList.Add(Block);
            }

            return Blocks;
        }

        internal static IBlockList<TNodeInterface, TNode> CreateBlockList(IList<IBlock<TNodeInterface, TNode>> nodeBlockList)
        {
            if (nodeBlockList == null) throw new ArgumentNullException(nameof(nodeBlockList));
            if (nodeBlockList.Count == 0) throw new ArgumentException($"{nameof(nodeBlockList)} must have at least one block");

            foreach (IBlock<TNodeInterface, TNode> Block in nodeBlockList)
                if (Block.NodeList.Count == 0) throw new ArgumentException($"All blocks in {nameof(nodeBlockList)} must have at least one node");

            BlockList<TNodeInterface, TNode> Blocks = new BlockList<TNodeInterface, TNode>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = nodeBlockList;

            return Blocks;
        }

        internal static IBlockList<TNodeInterface, TNode> CreateBlockListCopy(IBlockList<TNodeInterface, TNode> blockList)
        {
            if (blockList == null)
                return CreateEmptyBlockList();

            BlockList<TNodeInterface, TNode> Result = new BlockList<TNodeInterface, TNode>();
            Result.Documentation = NodeHelper.CreateDocumentationCopy(blockList.Documentation);
            Result.NodeBlockList = new List<IBlock<TNodeInterface, TNode>>();

            for (int BlockIndex = 0; BlockIndex < blockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<TNodeInterface, TNode> Block = blockList.NodeBlockList[BlockIndex];

                Block<TNodeInterface, TNode> NewBlock = new Block<TNodeInterface, TNode>();
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

                List<TNodeInterface> NewNodeList = new List<TNodeInterface>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    TNodeInterface Item = Block.NodeList[Index];
                    TNodeInterface NewItem = NodeHelper.DeepCloneNode(Item, cloneCommentGuid: false) as TNodeInterface;

                    Debug.Assert(NewItem != null, $"The clone is always a {nameof(TNodeInterface)}");
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                Result.NodeBlockList.Add(NewBlock);
            }

            return Result;
        }

        internal static IBlock<TNodeInterface, TNode> CreateBlock(IList<TNodeInterface> nodeList)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));
            if (nodeList.Count == 0) throw new ArgumentException($"{nameof(nodeList)} must be have at least one node");

            return CreateBlock(nodeList, ReplicationStatus.Normal, NodeHelper.CreateEmptyPattern(), NodeHelper.CreateEmptyIdentifier());
        }

        internal static IBlock<TNodeInterface, TNode> CreateBlock(IList<TNodeInterface> nodeList, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));
            if (nodeList.Count == 0) throw new ArgumentException($"{nameof(nodeList)} must be have at least one node");

            Block<TNodeInterface, TNode> Block = new Block<TNodeInterface, TNode>();
            Block.Documentation = NodeHelper.CreateEmptyDocumentation();
            Block.NodeList = nodeList;
            Block.Replication = replication;
            Block.ReplicationPattern = replicationPattern;
            Block.SourceIdentifier = sourceIdentifier;

            return Block;
        }

        internal static bool IsSimple(IBlockList<TNodeInterface, TNode> blockList)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));

            return blockList.NodeBlockList.Count == 1 && blockList.NodeBlockList[0].NodeList.Count == 1;
        }
    }
}