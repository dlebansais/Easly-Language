namespace BaseNode
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a global replicate definition.
    /// /Doc/Nodes/GlobalReplicate.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class GlobalReplicate : Node
    {
        /// <summary>
        /// Gets or sets the replicate name.
        /// </summary>
        public virtual Name ReplicateName { get; set; } = null!;

        /// <summary>
        /// Gets or sets patterns to use in the replication.
        /// </summary>
        public virtual IList<Pattern> Patterns { get; set; } = null!;
    }
}
