namespace BaseNodeHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using BaseNode;

    /// <summary>
    /// Provides methods to manipulate block lists of nodes.
    /// </summary>
    public static partial class NodeTreeHelperBlockList
    {
        /// <summary>
        /// Creates a new instance of a <see cref="IBlock"/> with provided values.
        /// </summary>
        /// <param name="node">The node for which the block is created.</param>
        /// <param name="propertyName">The property name in the block.</param>
        /// <param name="replication">The replication status.</param>
        /// <param name="replicationPattern">The replication pattern.</param>
        /// <param name="sourceIdentifier">The source identifier.</param>
        /// <returns>The created instance.</returns>
        public static IBlock CreateBlock(Node node, string propertyName, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            Type NodeType = node.GetType();
            PropertyInfo Property = SafeType.GetProperty(NodeType, propertyName);

            Type PropertyType = Property.PropertyType;
            Debug.Assert(NodeTreeHelper.IsBlockListType(PropertyType));

            return CreateBlock(PropertyType, replication, replicationPattern, sourceIdentifier);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IBlock"/> with provided values.
        /// </summary>
        /// <param name="blockList">The block list for which the block is created.</param>
        /// <param name="replication">The replication status.</param>
        /// <param name="replicationPattern">The replication pattern.</param>
        /// <param name="sourceIdentifier">The source identifier.</param>
        /// <returns>The created instance.</returns>
        public static IBlock CreateBlock(IBlockList blockList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
        {
            if (blockList == null) throw new ArgumentNullException(nameof(blockList));
            if (replicationPattern == null) throw new ArgumentNullException(nameof(replicationPattern));
            if (sourceIdentifier == null) throw new ArgumentNullException(nameof(sourceIdentifier));

            return CreateBlock(blockList.GetType(), replication, replicationPattern, sourceIdentifier);
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IBlock"/> with provided values.
        /// </summary>
        /// <param name="propertyType">The type of block list for which the block is created.</param>
        /// <param name="replication">The replication status.</param>
        /// <param name="replicationPattern">The replication pattern.</param>
        /// <param name="sourceIdentifier">The source identifier.</param>
        /// <returns>The created instance.</returns>
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
            string BlockTypeFullName = SafeType.FullName(BlockType);

            IBlock NewBlock = SafeType.CreateInstance<IBlock>(BlockType.Assembly, BlockTypeFullName);

            Document EmptyComment = NodeHelper.CreateEmptyDocumentation();

            PropertyInfo DocumentationPropertyInfo = SafeType.GetProperty(BlockType, nameof(Node.Documentation));

            DocumentationPropertyInfo.SetValue(NewBlock, EmptyComment);

            Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });

            string FullName = SafeType.FullName(NodeListType);

            IList NewNodeList = SafeType.CreateInstance<IList>(NodeListType.Assembly, FullName);

            PropertyInfo ReplicationPropertyInfo = SafeType.GetProperty(BlockType, nameof(IBlock.Replication));

            ReplicationPropertyInfo.SetValue(NewBlock, replication);

            PropertyInfo NodeListPropertyInfo = SafeType.GetProperty(BlockType, nameof(IBlock.NodeList));

            NodeListPropertyInfo.SetValue(NewBlock, NewNodeList);

            PropertyInfo ReplicationPatternPropertyInfo = SafeType.GetProperty(BlockType, nameof(IBlock.ReplicationPattern));

            ReplicationPatternPropertyInfo.SetValue(NewBlock, replicationPattern);

            PropertyInfo SourceIdentifierPropertyInfo = SafeType.GetProperty(BlockType, nameof(IBlock.SourceIdentifier));

            SourceIdentifierPropertyInfo.SetValue(NewBlock, sourceIdentifier);

            return NewBlock;
        }
    }
}
