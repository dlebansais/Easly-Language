namespace BaseNode;

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a block of nodes.
/// </summary>
public interface IBlock
{
    /// <summary>
    /// Gets the documentation.
    /// </summary>
    Document Documentation { get; }

    /// <summary>
    /// Gets the list of nodes in the block.
    /// </summary>
    IList NodeList { get; }

    /// <summary>
    /// Gets how nodes are replicated.
    /// </summary>
    ReplicationStatus Replication { get; }

    /// <summary>
    /// Gets the pattern to use for replications.
    /// </summary>
    Pattern ReplicationPattern { get; }

    /// <summary>
    /// Gets the source to use for replication.
    /// </summary>
    Identifier SourceIdentifier { get; }
}

/// <summary>
/// Represents a block of nodes.
/// </summary>
/// <typeparam name="TNode">Type of the node.</typeparam>
public interface IBlock<TNode>
    where TNode : Node
{
    /// <summary>
    /// Gets the documentation.
    /// </summary>
    Document Documentation { get; }

    /// <summary>
    /// Gets the list of nodes in the block.
    /// </summary>
    IList<TNode> NodeList { get; }

    /// <summary>
    /// Gets how nodes are replicated.
    /// </summary>
    ReplicationStatus Replication { get; }

    /// <summary>
    /// Gets the pattern to use for replications.
    /// </summary>
    Pattern ReplicationPattern { get; }

    /// <summary>
    /// Gets the source to use for replication.
    /// </summary>
    Identifier SourceIdentifier { get; }
}

/// <inheritdoc/>
[System.Serializable]
public class Block<TNode> : IBlock<TNode>, IBlock
    where TNode : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Block()
#pragma warning restore SA1600 // Elements should be documented
    {
        Documentation = default!;
        NodeList = default!;
        Replication = default!;
        ReplicationPattern = default!;
        SourceIdentifier = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Block{TNode}"/> class.
    /// </summary>
    /// <param name="documentation">The block documentation.</param>
    /// <param name="nodeList">The list of nodes in the block.</param>
    /// <param name="replication">How nodes are replicated.</param>
    /// <param name="replicationPattern">The pattern to use for replications.</param>
    /// <param name="sourceIdentifier">The source to use for replication.</param>
    internal Block(Document documentation, IList<TNode> nodeList, ReplicationStatus replication, Pattern replicationPattern, Identifier sourceIdentifier)
    {
        Documentation = documentation;
        NodeList = nodeList;
        Replication = replication;
        ReplicationPattern = replicationPattern;
        SourceIdentifier = sourceIdentifier;
    }

    /// <summary>
    /// Gets or sets the documentation.
    /// </summary>
    public virtual Document Documentation { get; set; }

    /// <summary>
    /// Gets or sets the list of nodes in the block.
    /// </summary>
    public virtual IList<TNode> NodeList { get; set; }

    /// <inheritdoc/>
    IList IBlock.NodeList { get { return (IList)NodeList; } }

    /// <summary>
    /// Gets or sets how nodes are replicated.
    /// </summary>
    public virtual ReplicationStatus Replication { get; set; }

    /// <summary>
    /// Gets or sets the pattern to use for replications.
    /// </summary>
    public virtual Pattern ReplicationPattern { get; set; }

    /// <summary>
    /// Gets or sets the source to use for replication.
    /// </summary>
    public virtual Identifier SourceIdentifier { get; set; }
}
