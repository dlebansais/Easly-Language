namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate block lists of nodes.
    /// </summary>
    public static class BlockListHelper
    {
        /// <inheritdoc cref="BlockListHelper{TNode}.CreateEmptyBlockList"/>
        public static IBlockList<TNode> CreateEmptyBlockList<TNode>()
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateEmptyBlockList();
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateSimpleBlockList"/>
        public static IBlockList<TNode> CreateSimpleBlockList<TNode>(TNode node)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateSimpleBlockList(node);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlockList(IList{TNode})"/>
        public static IBlockList<TNode> CreateBlockList<TNode>(IList<TNode> nodeList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlockList(nodeList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlockList(IList{IBlock{TNode}})"/>
        public static IBlockList<TNode> CreateBlockList<TNode>(IList<IBlock<TNode>> nodeBlockList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlockList(nodeBlockList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlockListCopy"/>
        public static IBlockList<TNode> CreateBlockListCopy<TNode>(IBlockList<TNode> blockList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlockListCopy(blockList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlock(IList{TNode})"/>
        public static IBlock<TNode> CreateBlock<TNode>(IList<TNode> nodeList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlock(nodeList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlock(IList{TNode}, ReplicationStatus, Pattern, Identifier)"/>
        public static IBlock<TNode> CreateBlock<TNode>(IList<TNode> nodeList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
            where TNode : Node
        {
            return BlockListHelper<TNode>.CreateBlock(nodeList, replication, replicationPattern, sourceIdentifier);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.IsSimple"/>
        public static bool IsSimple<TNode>(IBlockList<TNode> blockList)
            where TNode : Node
        {
            return BlockListHelper<TNode>.IsSimple(blockList);
        }
    }
}