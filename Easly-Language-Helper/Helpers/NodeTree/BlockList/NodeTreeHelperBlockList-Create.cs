namespace BaseNodeHelper;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BaseNode;
using Contracts;

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);
        Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);

        GetBlockListInternal(Node, PropertyName, out _, out Type PropertyType, out _);

        return CreateBlockInternal(PropertyType, replication, ReplicationPattern, SourceIdentifier);
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
        Contract.RequireNotNull(blockList, out IBlockList BlockList);
        Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);
        Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);

        Type BlockListType = BlockList.GetType();

        if (!NodeTreeHelper.IsSomeBlockType(BlockListType))
            throw new ArgumentException($"{nameof(blockList)} must be a {typeof(BlockList<>)} type");

        return CreateBlockInternal(BlockListType, replication, ReplicationPattern, SourceIdentifier);
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
        Contract.RequireNotNull(propertyType, out Type PropertyType);
        Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);
        Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);

        if (!NodeTreeHelper.IsSomeBlockType(PropertyType))
            throw new ArgumentException($"{nameof(propertyType)} must be a {typeof(Block<>)} or {typeof(BlockList<>)} type");

        return CreateBlockInternal(PropertyType, replication, ReplicationPattern, SourceIdentifier);
    }

    private static IBlock CreateBlockInternal(Type propertyType, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
    {
        Debug.Assert(NodeTreeHelper.IsSomeBlockType(propertyType));
        Debug.Assert(propertyType.IsGenericType);

        Type[] TypeArguments = propertyType.GetGenericArguments();

        Type BlockType = typeof(Block<>).MakeGenericType(TypeArguments);
        string BlockTypeFullName = SafeType.FullName(BlockType);

        IBlock NewBlock = SafeType.CreateInstance<IBlock>(BlockType.Assembly, BlockTypeFullName);

        Document EmptyComment = NodeHelper.CreateEmptyDocumentation();

        PropertyInfo DocumentationPropertyInfo = SafeType.GetProperty(BlockType, nameof(Node.Documentation));

        DocumentationPropertyInfo.SetValue(NewBlock, EmptyComment);

        Type NodeListType = typeof(List<>).MakeGenericType(new Type[] { TypeArguments[0] });

        string FullName = SafeType.FullName(NodeListType);

        IList NewNodeList = SafeType.CreateInstanceFromDefaultConstructor<IList>(NodeListType.Assembly, FullName);

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
