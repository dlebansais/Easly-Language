namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static class BlockListHelper<TNodeInterface, TNode>
        where TNodeInterface : class, INode
        where TNode : Node, TNodeInterface
    {
        public static IBlockList<TNodeInterface, TNode> CreateEmptyBlockList()
        {
            return CreateBlockList(new List<TNodeInterface>());
        }

        public static IBlockList<TNodeInterface, TNode> CreateSimpleBlockList(TNodeInterface node)
        {
            List<TNodeInterface> NodeList = new List<TNodeInterface>();
            NodeList.Add(node);

            return CreateBlockList(NodeList);
        }

        public static IBlockList<TNodeInterface, TNode> CreateBlockList(IList<TNodeInterface> nodeList)
        {
            if (nodeList == null)
                throw new ArgumentNullException(nameof(nodeList));

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

        public static IBlockList<TNodeInterface, TNode> CreateBlockList(IList<IBlock<TNodeInterface, TNode>> nodeBlockList)
        {
            if (nodeBlockList == null)
                throw new ArgumentNullException(nameof(nodeBlockList));

            Debug.Assert(nodeBlockList.Count > 0, $"{nameof(nodeBlockList)} must have at least one block");

            foreach (IBlock<TNodeInterface, TNode> Block in nodeBlockList)
                Debug.Assert(Block.NodeList.Count > 0, "A block must have at least one node");

            BlockList<TNodeInterface, TNode> Blocks = new BlockList<TNodeInterface, TNode>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = nodeBlockList;

            return Blocks;
        }

        public static IBlockList<TNodeInterface, TNode> CreateBlockListCopy(IBlockList<TNodeInterface, TNode> blockList)
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

                    Debug.Assert(NewItem != null, $"The clone has to be a {nameof(TNodeInterface)}");
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                Result.NodeBlockList.Add(NewBlock);
            }

            return Result;
        }

        public static IBlock<TNodeInterface, TNode> CreateBlock(IList<TNodeInterface> nodeList)
        {
            if (nodeList == null)
                throw new ArgumentNullException(nameof(nodeList));

            Debug.Assert(nodeList.Count > 0, "A block must be created with at least one node");

            return CreateBlock(nodeList, ReplicationStatus.Normal, NodeHelper.CreateEmptyPattern(), NodeHelper.CreateEmptyIdentifier());
        }

        public static IBlock<TNodeInterface, TNode> CreateBlock(IList<TNodeInterface> nodeList, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            if (nodeList == null)
                throw new ArgumentNullException(nameof(nodeList));

            Debug.Assert(nodeList.Count > 0, "A block must be created with at least one node");

            Block<TNodeInterface, TNode> Block = new Block<TNodeInterface, TNode>();
            Block.Documentation = NodeHelper.CreateEmptyDocumentation();
            Block.NodeList = nodeList;
            Block.Replication = replication;
            Block.ReplicationPattern = replicationPattern;
            Block.SourceIdentifier = sourceIdentifier;

            return Block;
        }

        public static bool IsSimple(IBlockList<TNodeInterface, TNode> blockList)
        {
            if (blockList == null)
                throw new ArgumentNullException(nameof(blockList));

            return blockList.NodeBlockList.Count == 1 && blockList.NodeBlockList[0].NodeList.Count == 1;
        }
    }
}