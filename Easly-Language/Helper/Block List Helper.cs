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

        public static IBlockList<IN, N> CreateSimpleBlockList(IN Node)
        {
            List<IN> NodeList = new List<IN>();
            NodeList.Add(Node);

            return CreateBlockList(NodeList);
        }

        public static IBlockList<IN, N> CreateBlockList(IList<IN> NodeList)
        {
            BlockList<IN, N> Blocks = new BlockList<IN, N>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = new List<IBlock<IN, N>>();

            if (NodeList.Count > 0)
            {
                IBlock<IN, N> Block = CreateBlock(NodeList);
                Blocks.NodeBlockList.Add(Block);
            }

            return Blocks;
        }

        public static IBlockList<IN, N> CreateBlockList(IList<IBlock<IN, N>> NodeBlockList)
        {
            Debug.Assert(NodeBlockList.Count > 0);

            foreach (IBlock<IN, N> Block in NodeBlockList)
                Debug.Assert(Block.NodeList.Count > 0);

            BlockList<IN, N> Blocks = new BlockList<IN, N>();
            Blocks.Documentation = NodeHelper.CreateEmptyDocumentation();
            Blocks.NodeBlockList = NodeBlockList;

            return Blocks;
        }

        public static IBlock<IN, N> CreateBlock(IList<IN> NodeList)
        {
            Debug.Assert(NodeList.Count > 0);

            return CreateBlock(NodeList, ReplicationStatus.Normal, NodeHelper.CreateEmptyPattern(), NodeHelper.CreateEmptyIdentifier());
        }

        public static IBlock<IN, N> CreateBlock(IList<IN> NodeList, ReplicationStatus Replication, IPattern ReplicationPattern, IIdentifier SourceIdentifier)
        {
            Debug.Assert(NodeList.Count > 0);

            Block<IN, N> Block = new Block<IN, N>();
            Block.Documentation = NodeHelper.CreateEmptyDocumentation();
            Block.NodeList = NodeList;
            Block.Replication = Replication;
            Block.ReplicationPattern = ReplicationPattern;
            Block.SourceIdentifier = SourceIdentifier;

            return Block;
        }
    }
}
