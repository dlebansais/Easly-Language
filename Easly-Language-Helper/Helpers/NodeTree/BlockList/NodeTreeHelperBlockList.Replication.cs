#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;
    using Easly;

    public static partial class NodeTreeHelperBlockList
    {
        public static bool IsBlockPatternNode(Node node, string propertyName, int blockIndex, Pattern replicationPattern)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return false;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return false;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock? Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            if (Block == null)
                return false;

            return IsPatternNode(Block, replicationPattern);
        }

        public static bool IsPatternNode(IBlock block, Pattern replicationPattern)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));

            return replicationPattern == block.ReplicationPattern;
        }

        public static string GetPattern(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Pattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            if (ReplicationPattern == null)
                return null!;

            return NodeTreeHelper.GetString(ReplicationPattern, nameof(Pattern.Text));
        }

        public static void SetPattern(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Pattern ReplicationPattern = block.ReplicationPattern;
            Debug.Assert(ReplicationPattern != null);

            if (ReplicationPattern == null)
                return;

            NodeTreeHelper.SetString(ReplicationPattern, nameof(Pattern.Text), text);
        }

        public static bool IsBlockSourceNode(Node node, string propertyName, int blockIndex, Identifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (blockIndex < 0) throw new ArgumentOutOfRangeException(nameof(blockIndex));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return false;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return false;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(PropertyType != null);

            if (PropertyType == null)
                return false;

            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            IBlockList? BlockList = Property.GetValue(node) as IBlockList;
            Debug.Assert(BlockList != null);

            if (BlockList == null)
                return false;

            IList NodeBlockList = BlockList.NodeBlockList;
            Debug.Assert(NodeBlockList != null);

            if (NodeBlockList == null)
                return false;

            Debug.Assert(blockIndex < NodeBlockList.Count);
            if (blockIndex >= NodeBlockList.Count) throw new ArgumentOutOfRangeException(nameof(blockIndex));

            IBlock? Block = NodeBlockList[blockIndex] as IBlock;
            Debug.Assert(Block != null);

            if (Block == null)
                return false;

            return IsSourceNode(Block, sourceIdentifier);
        }

        public static bool IsSourceNode(IBlock block, Identifier sourceIdentifier)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return sourceIdentifier == block.SourceIdentifier;
        }

        public static string GetSource(IBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Identifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            if (SourceIdentifier == null)
                return null!;

            return NodeTreeHelper.GetString(SourceIdentifier, nameof(Identifier.Text));
        }

        public static void SetSource(IBlock block, string text)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (text == null) throw new ArgumentNullException(nameof(text));

            Identifier SourceIdentifier = block.SourceIdentifier;
            Debug.Assert(SourceIdentifier != null);

            if (SourceIdentifier == null)
                return;

            NodeTreeHelper.SetString(SourceIdentifier, nameof(Identifier.Text), text);
        }

        public static void SetReplication(IBlock block, ReplicationStatus replication)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            Type BlockType = block.GetType();
            Debug.Assert(BlockType != null);

            if (BlockType == null)
                return;

            PropertyInfo? ReplicationPropertyInfo = BlockType.GetProperty(nameof(IBlock.Replication));
            Debug.Assert(ReplicationPropertyInfo != null);

            if (ReplicationPropertyInfo == null)
                return;

            ReplicationPropertyInfo.SetValue(block, replication);
        }
    }
}
