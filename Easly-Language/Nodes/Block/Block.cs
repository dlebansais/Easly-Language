namespace BaseNode
{
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
        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public virtual Document Documentation { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of nodes in the block.
        /// </summary>
        public virtual IList<TNode> NodeList { get; set; } = null!;

        /// <inheritdoc/>
        IList IBlock.NodeList { get { return (IList)NodeList; } }

        /// <summary>
        /// Gets or sets how nodes are replicated.
        /// </summary>
        public virtual ReplicationStatus Replication { get; set; } = ReplicationStatus.Normal;

        /// <summary>
        /// Gets or sets the pattern to use for replications.
        /// </summary>
        public virtual Pattern ReplicationPattern { get; set; } = null!;

        /// <summary>
        /// Gets or sets the source to use for replication.
        /// </summary>
        public virtual Identifier SourceIdentifier { get; set; } = null!;
    }
}
