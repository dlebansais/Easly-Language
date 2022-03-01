namespace BaseNodeHelper;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using BaseNode;
using Contracts;
using NotNullReflection;
using ArgumentException = System.ArgumentException;

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

        Type BlockListType = Type.FromGetType(BlockList);

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

        Type BlockType = Type.FromTypeof<Block<Node>>().GetGenericTypeDefinition().MakeGenericType(TypeArguments);
        IBlock NewBlock = NodeHelper.CreateInstance<IBlock>(BlockType);

        Document EmptyComment = NodeHelper.CreateEmptyDocument();

        PropertyInfo DocumentationPropertyInfo = BlockType.GetProperty(nameof(Node.Documentation));

        DocumentationPropertyInfo.SetValue(NewBlock, EmptyComment);

        Type NodeListType = Type.FromTypeof<List<Node>>().GetGenericTypeDefinition().MakeGenericType(new Type[] { TypeArguments[0] });

        IList NewNodeList = NodeHelper.CreateInstanceFromDefaultConstructor<IList>(NodeListType);

        PropertyInfo ReplicationPropertyInfo = BlockType.GetProperty(nameof(IBlock.Replication));

        ReplicationPropertyInfo.SetValue(NewBlock, replication);

        PropertyInfo NodeListPropertyInfo = BlockType.GetProperty(nameof(IBlock.NodeList));

        NodeListPropertyInfo.SetValue(NewBlock, NewNodeList);

        PropertyInfo ReplicationPatternPropertyInfo = BlockType.GetProperty(nameof(IBlock.ReplicationPattern));

        ReplicationPatternPropertyInfo.SetValue(NewBlock, replicationPattern);

        PropertyInfo SourceIdentifierPropertyInfo = BlockType.GetProperty(nameof(IBlock.SourceIdentifier));

        SourceIdentifierPropertyInfo.SetValue(NewBlock, sourceIdentifier);

        return NewBlock;
    }
}
