namespace BaseNodeHelper;

using System.Collections;
using BaseNode;
using Contracts;
using NotNullReflection;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);
        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock Block = SafeList.ItemAt<IBlock>(NodeBlockList, blockIndex);

        return IsPatternNodeInternal(Block, ReplicationPattern);
    }

    /// <summary>
    /// Checks whether a replication pattern in a block has the provided value.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="replicationPattern">The replication pattern.</param>
    /// <returns>True if the replication pattern has the provided value; otherwise, false.</returns>
    public static bool IsPatternNode(IBlock block, Pattern replicationPattern)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);

        return IsPatternNodeInternal(Block, ReplicationPattern);
    }

    /// <summary>
    /// Gets the replication pattern string in a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <returns>The replication pattern string.</returns>
    public static string GetPattern(IBlock block)
    {
        Contract.RequireNotNull(block, out IBlock Block);

        Pattern ReplicationPattern = Block.ReplicationPattern;

        return NodeTreeHelper.GetString(ReplicationPattern, nameof(Pattern.Text));
    }

    /// <summary>
    /// Sets the replication pattern string in a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="text">The replication pattern string.</param>
    public static void SetPattern(IBlock block, string text)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(text, out string Text);

        Pattern ReplicationPattern = Block.ReplicationPattern;

        NodeTreeHelper.SetString(ReplicationPattern, nameof(Pattern.Text), Text);
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
        Contract.RequireNotNull(node, out Node Node);
        Contract.RequireNotNull(propertyName, out string PropertyName);
        Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);

        GetBlockListInternal(Node, PropertyName, out _, out _, out IBlockList BlockList);
        IList NodeBlockList = BlockList.NodeBlockList;

        if (blockIndex < 0 || blockIndex >= NodeBlockList.Count)
            throw new ArgumentOutOfRangeException(nameof(blockIndex));

        IBlock Block = SafeList.ItemAt<IBlock>(NodeBlockList, blockIndex);

        return IsSourceNodeInternal(Block, SourceIdentifier);
    }

    /// <summary>
    /// Checks whether a source identifier in a block has the provided value.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="sourceIdentifier">The source identifier.</param>
    /// <returns>True if the source identifier has the provided value; otherwise, false.</returns>
    public static bool IsSourceNode(IBlock block, Identifier sourceIdentifier)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);

        return IsSourceNodeInternal(Block, SourceIdentifier);
    }

    /// <summary>
    /// Gets the source identifier string in a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <returns>The source identifier string.</returns>
    public static string GetSource(IBlock block)
    {
        Contract.RequireNotNull(block, out IBlock Block);

        Identifier SourceIdentifier = Block.SourceIdentifier;

        return NodeTreeHelper.GetString(SourceIdentifier, nameof(Identifier.Text));
    }

    /// <summary>
    /// Sets the source identifier string in a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="text">The source identifier string.</param>
    public static void SetSource(IBlock block, string text)
    {
        Contract.RequireNotNull(block, out IBlock Block);
        Contract.RequireNotNull(text, out string Text);

        Identifier SourceIdentifier = Block.SourceIdentifier;

        NodeTreeHelper.SetString(SourceIdentifier, nameof(Identifier.Text), Text);
    }

    /// <summary>
    /// Sets the replication status of a block.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <param name="replication">The replication status.</param>
    public static void SetReplication(IBlock block, ReplicationStatus replication)
    {
        Contract.RequireNotNull(block, out IBlock Block);

        Type BlockType = Type.FromGetType(Block);
        PropertyInfo ReplicationPropertyInfo = BlockType.GetProperty(nameof(IBlock.Replication));

        ReplicationPropertyInfo.SetValue(block, replication);
    }

    private static bool IsPatternNodeInternal(IBlock block, Pattern replicationPattern)
    {
        return replicationPattern == block.ReplicationPattern;
    }

    private static bool IsSourceNodeInternal(IBlock block, Identifier sourceIdentifier)
    {
        return sourceIdentifier == block.SourceIdentifier;
    }
}
