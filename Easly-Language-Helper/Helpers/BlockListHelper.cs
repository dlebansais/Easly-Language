namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using BaseNode;
    using Contracts;

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
            Contract.RequireNotNull(node, out TNode Node);

            return BlockListHelper<TNode>.CreateSimpleBlockList(Node);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlockListFromNodeList"/>
        public static IBlockList<TNode> CreateBlockList<TNode>(IList<TNode> nodeList)
            where TNode : Node
        {
            Contract.RequireNotNull(nodeList, out IList<TNode> NodeList);

            return BlockListHelper<TNode>.CreateBlockListFromNodeList(NodeList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlockListFromBlockList"/>
        public static IBlockList<TNode> CreateBlockList<TNode>(IList<IBlock<TNode>> nodeBlockList)
            where TNode : Node
        {
            Contract.RequireNotNull(nodeBlockList, out IList<IBlock<TNode>> NodeBlockList);

            return BlockListHelper<TNode>.CreateBlockListFromBlockList(NodeBlockList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlockListCopy"/>
        public static IBlockList<TNode> CreateBlockListCopy<TNode>(IBlockList<TNode> blockList)
            where TNode : Node
        {
            Contract.RequireNotNull(blockList, out IBlockList<TNode> BlockList);

            return BlockListHelper<TNode>.CreateBlockListCopy(BlockList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlock(IList{TNode})"/>
        public static IBlock<TNode> CreateBlock<TNode>(IList<TNode> nodeList)
            where TNode : Node
        {
            Contract.RequireNotNull(nodeList, out IList<TNode> NodeList);

            return BlockListHelper<TNode>.CreateBlock(NodeList);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.CreateBlock(IList{TNode}, ReplicationStatus, Pattern, Identifier)"/>
        public static IBlock<TNode> CreateBlock<TNode>(IList<TNode> nodeList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
            where TNode : Node
        {
            Contract.RequireNotNull(nodeList, out IList<TNode> NodeList);
            Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);
            Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);

            return BlockListHelper<TNode>.CreateBlock(NodeList, replication, ReplicationPattern, SourceIdentifier);
        }

        /// <inheritdoc cref="BlockListHelper{TNode}.IsSimple"/>
        public static bool IsSimple<TNode>(IBlockList<TNode> blockList)
            where TNode : Node
        {
            Contract.RequireNotNull(blockList, out IBlockList<TNode> BlockList);

            return BlockListHelper<TNode>.IsSimple(BlockList);
        }
    }
}