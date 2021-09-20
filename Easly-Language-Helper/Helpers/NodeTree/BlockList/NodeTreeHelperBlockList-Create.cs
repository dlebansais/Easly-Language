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
        public static IBlock CreateBlock(Node node, string propertyName, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            Debug.Assert(NodeType != null);

            if (NodeType == null)
                return null!;

            PropertyInfo? Property = NodeType.GetProperty(propertyName);
            Debug.Assert(Property != null);

            if (Property == null)
                return null!;

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            return CreateBlock(PropertyType, replication, replicationPattern, sourceIdentifier);
        }

        public static IBlock CreateBlock(IBlockList blockList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return CreateBlock(blockList.GetType(), replication, replicationPattern, sourceIdentifier);
        }

        public static IBlock CreateBlock(Type propertyType, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (propertyType == null) throw new ArgumentNullException(nameof(propertyType));
            if (!propertyType.IsGenericType) throw new ArgumentException($"{nameof(propertyType)} must be a generic type");
            Type GenericTypeDefinition = propertyType.GetGenericTypeDefinition();
            if (GenericTypeDefinition != typeof(IBlockList<>) && GenericTypeDefinition != typeof(BlockList<>) && GenericTypeDefinition != typeof(IBlock<>) && GenericTypeDefinition != typeof(Block<>)) throw new ArgumentException($"{nameof(propertyType)} must be a block or block list type");
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type[] TypeArguments = propertyType.GetGenericArguments();

            Type BlockType = typeof(Block<>).MakeGenericType(TypeArguments);
            Debug.Assert(BlockType != null);

            if (BlockType == null)
                return null!;

            string? BlockTypeFullName = BlockType.FullName;
            Debug.Assert(BlockTypeFullName != null);

            if (BlockTypeFullName == null)
                return null!;

            IBlock? NewBlock = BlockType.Assembly.CreateInstance(BlockTypeFullName) as IBlock;
            Debug.Assert(NewBlock != null);

            if (NewBlock == null)
                return null!;

            Document EmptyComment = NodeHelper.CreateEmptyDocumentation();

            PropertyInfo? DocumentationPropertyInfo = BlockType.GetProperty(nameof(Node.Documentation));
            Debug.Assert(DocumentationPropertyInfo != null);

            if (DocumentationPropertyInfo == null)
                return null!;

            DocumentationPropertyInfo.SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });

            string? FullName = NodeListType.FullName;
            Debug.Assert(FullName != null);

            if (FullName == null)
                return null!;

            IList? NewNodeList = NodeListType.Assembly.CreateInstance(FullName) as IList;
            Debug.Assert(NewNodeList != null);

            if (NewNodeList == null)
                return null!;

            PropertyInfo? ReplicationPropertyInfo = BlockType.GetProperty(nameof(IBlock.Replication));
            Debug.Assert(ReplicationPropertyInfo != null);

            if (ReplicationPropertyInfo == null)
                return null!;

            ReplicationPropertyInfo.SetValue(NewBlock, replication);

            PropertyInfo? NodeListPropertyInfo = BlockType.GetProperty(nameof(IBlock.NodeList));
            Debug.Assert(NodeListPropertyInfo != null);

            if (NodeListPropertyInfo == null)
                return null!;

            NodeListPropertyInfo.SetValue(NewBlock, NewNodeList);

            PropertyInfo? ReplicationPatternPropertyInfo = BlockType.GetProperty(nameof(IBlock.ReplicationPattern));
            Debug.Assert(ReplicationPatternPropertyInfo != null);

            if (ReplicationPatternPropertyInfo == null)
                return null!;

            ReplicationPatternPropertyInfo.SetValue(NewBlock, replicationPattern);

            PropertyInfo? SourceIdentifierPropertyInfo = BlockType.GetProperty(nameof(IBlock.SourceIdentifier));
            Debug.Assert(SourceIdentifierPropertyInfo != null);

            if (SourceIdentifierPropertyInfo == null)
                return null!;

            SourceIdentifierPropertyInfo.SetValue(NewBlock, sourceIdentifier);

            return NewBlock;
        }
    }
}
