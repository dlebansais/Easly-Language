#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public static class BlockListHelper
    {
        public static IBlockList<TNode> CreateEmptyBlockList<TNode>()
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateEmptyBlockList();
        }

        public static IBlockList<TNode> CreateSimpleBlockList<TNode>(TNode node)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateSimpleBlockList(node);
        }

        public static IBlockList<TNode> CreateBlockList<TNode>(IList<TNode> nodeList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlockList(nodeList);
        }

        public static IBlockList<TNode> CreateBlockList<TNode>(IList<IBlock<TNode>> nodeBlockList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlockList(nodeBlockList);
        }

        public static IBlockList<TNode> CreateBlockListCopy<TNode>(IBlockList<TNode> blockList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlockListCopy(blockList);
        }

        public static IBlock<TNode> CreateBlock<TNode>(IList<TNode> nodeList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlock(nodeList);
        }

        public static IBlock<TNode> CreateBlock<TNode>(IList<TNode> nodeList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlock(nodeList, replication, replicationPattern, sourceIdentifier);
        }

        public static bool IsSimple<TNode>(IBlockList<TNode> blockList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.IsSimple(blockList);
        }
    }
}