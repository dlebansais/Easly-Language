namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate block lists of nodes.
    /// </summary>
    public static partial class NodeTreeHelperBlockList
    {
        /// <summary>
        /// Checks whether a replication pattern of a block in a property of a node that is a block list has the provided value.
        /// </summary>
        /// <param name="node">The node with the property to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">The block index.</param>
        /// <param name="replicationPattern">The replication pattern to check.</param>
        /// <returns>True if the replication pattern in the block list has the provided value; otherwise, false.</returns>
        public static bool IsBlockPatternNode(Node node, string propertyName, int blockIndex, Pattern replicationPattern)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            return IsPatternNode(Block, replicationPattern);
        }

        /// <summary>
        /// Checks whether a replication pattern in a block has the provided value.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="replicationPattern">The replication pattern.</param>
        /// <returns>True if the replication pattern has the provided value; otherwise, false.</returns>
        public static bool IsPatternNode(IBlock block, Pattern replicationPattern)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            return replicationPattern == block.ReplicationPattern;
        }

        /// <summary>
        /// Gets the replication pattern string in a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <returns>The replication pattern string.</returns>
        public static string GetPattern(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Pattern ReplicationPattern = block.ReplicationPattern;

            return NodeTreeHelper.GetString(ReplicationPattern, nameof(Pattern.Text));
        }

        /// <summary>
        /// Sets the replication pattern string in a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="text">The replication pattern string.</param>
        public static void SetPattern(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Pattern ReplicationPattern = block.ReplicationPattern;

            NodeTreeHelper.SetString(ReplicationPattern, nameof(Pattern.Text), text);
        }

        /// <summary>
        /// Checks whether a source identifier of a block in a property of a node that is a block list has the provided value.
        /// </summary>
        /// <param name="node">The node with the property to check.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="blockIndex">The block index.</param>
        /// <param name="sourceIdentifier">The source identifier to check.</param>
        /// <returns>True if the source identifier in the block list has the provided value; otherwise, false.</returns>
        public static bool IsBlockSourceNode(Node node, string propertyName, int blockIndex, Identifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);
            Type PropertyType = Property.PropertyType;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList BlockList = SafeType.GetPropertyValue<IBlockList>(Property, node);

            IList NodeBlockList = BlockList.NodeBlockList;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock Block = SafeType.ItemAt<IBlock>(NodeBlockList, blockIndex);

            return IsSourceNode(Block, sourceIdentifier);
        }

        /// <summary>
        /// Checks whether a source identifier in a block has the provided value.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="sourceIdentifier">The source identifier.</param>
        /// <returns>True if the source identifier has the provided value; otherwise, false.</returns>
        public static bool IsSourceNode(IBlock block, Identifier sourceIdentifier)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return sourceIdentifier == block.SourceIdentifier;
        }

        /// <summary>
        /// Gets the source identifier string in a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <returns>The source identifier string.</returns>
        public static string GetSource(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Identifier SourceIdentifier = block.SourceIdentifier;

            return NodeTreeHelper.GetString(SourceIdentifier, nameof(Identifier.Text));
        }

        /// <summary>
        /// Sets the source identifier string in a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="text">The source identifier string.</param>
        public static void SetSource(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Identifier SourceIdentifier = block.SourceIdentifier;

            NodeTreeHelper.SetString(SourceIdentifier, nameof(Identifier.Text), text);
        }

        /// <summary>
        /// Sets the replication status of a block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="replication">The replication status.</param>
        public static void SetReplication(IBlock block, ReplicationStatus replication)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Type BlockType = block.GetType();

            PropertyInfo ReplicationPropertyInfo = SafeType.GetProperty(BlockType, nameof(IBlock.Replication));

            ReplicationPropertyInfo.SetValue(block, replication);
        }
    }
}
