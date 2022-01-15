namespace BaseNode;

using System.Collections.Generic;

/// <summary>
/// Represents a global replicate definition.
/// /Doc/Nodes/GlobalReplicate.md explains the semantic.
/// </summary>
[System.Serializable]
public class GlobalReplicate : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalReplicate"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="replicateName">The replicate name.</param>
    /// <param name="patterns">The patterns to use in the replication.</param>
    internal GlobalReplicate(Document documentation, Name replicateName, IList<Pattern> patterns)
        : base(documentation)
    {
        ReplicateName = replicateName;
        Patterns = patterns;
    }

    /// <summary>
    /// Gets or sets the replicate name.
    /// </summary>
    public virtual Name ReplicateName { get; set; }

    /// <summary>
    /// Gets or sets the patterns to use in the replication.
    /// </summary>
    public virtual IList<Pattern> Patterns { get; set; }
}
