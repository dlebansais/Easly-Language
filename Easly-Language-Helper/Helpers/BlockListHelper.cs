using BaseNode;
using System.Collections.Generic;
using System.Diagnostics;

namespace BaseNodeHelper
{
    public class BlockListHelper<IN, N>
        where IN : class, INode
        where N : Node, IN
    {
        public static IBlockList<IN, N> CreateEmptyBlockList()
        {
            return CreateBlockList(new List<IN>());
        }

        public static IBlockList<IN, N> CreateSimpleBlockList(IN node)
        {
            List<IN> NodeList = new List<IN>();
            NodeList.Add(node);

            return CreateBlockList(NodeList);
        }

        public static IBlockList<IN, N> CreateBlockList(IList<IN> nodeList)
        {
            BlockList<IN, N> Blocks = new BlockList<IN, N>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = new List<IBlock<IN, N>>();

            if (nodeList.Count > 0)
            {
                IBlock<IN, N> Block = CreateBlock(nodeList);
                Blocks.NodeBlockList.Add(Block);
            }

            return Blocks;
        }

        public static IBlockList<IN, N> CreateBlockList(IList<IBlock<IN, N>> nodeBlockList)
        {
            Debug.Assert(nodeBlockList.Count > 0);

            foreach (IBlock<IN, N> Block in nodeBlockList)
                Debug.Assert(Block.NodeList.Count > 0);

            BlockList<IN, N> Blocks = new BlockList<IN, N>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = nodeBlockList;

            return Blocks;
        }

        public static IBlockList<IN, N> CreateBlockListCopy(IBlockList<IN, N> blockList)
        {
            if (blockList == null)
                return CreateEmptyBlockList();

            BlockList<IN, N> Result = new BlockList<IN, N>();
            Result.Documentation = NodeHelper.CreateDocumentationCopy(blockList.Documentation);
            Result.NodeBlockList = new List<IBlock<IN, N>>();

            for (int BlockIndex = 0; BlockIndex < blockList.NodeBlockList.Count; BlockIndex++)
            {
                IBlock<IN, N> Block = blockList.NodeBlockList[BlockIndex];

                Block<IN, N> NewBlock = new Block<IN, N>();
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

                List<IN> NewNodeList = new List<IN>();
                for (int Index = 0; Index < Block.NodeList.Count; Index++)
                {
                    IN Item = Block.NodeList[Index];
                    IN NewItem = NodeHelper.DeepCloneNode(Item, cloneCommentGuid: false) as IN;

                    Debug.Assert(NewItem != null);
                    NewNodeList.Add(NewItem);
                }

                NewBlock.NodeList = NewNodeList;

                Result.NodeBlockList.Add(NewBlock);
            }

            return Result;
        }

        public static IBlock<IN, N> CreateBlock(IList<IN> nodeList)
        {
            Debug.Assert(nodeList.Count > 0);

            return CreateBlock(nodeList, ReplicationStatus.Normal, NodeHelper.CreateEmptyPattern(), NodeHelper.CreateEmptyIdentifier());
        }

        public static IBlock<IN, N> CreateBlock(IList<IN> nodeList, ReplicationStatus replication, IPattern replicationPattern, IIdentifier sourceIdentifier)
        {
            Debug.Assert(nodeList.Count > 0);

            Block<IN, N> Block = new Block<IN, N>();
            Block.Documentation = NodeHelper.CreateEmptyDocumentation();
            Block.NodeList = nodeList;
            Block.Replication = replication;
            Block.ReplicationPattern = replicationPattern;
            Block.SourceIdentifier = sourceIdentifier;

            return Block;
        }
    }
}
