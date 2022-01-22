namespace BaseNode;

using System.Collections.Generic;

/// <summary>
/// Represents a global replicate definition.
/// /Doc/Nodes/GlobalReplicate.md explains the semantic.
/// </summary>
[System.Serializable]
public class GlobalReplicate : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public GlobalReplicate()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ReplicateName = default!;
        Patterns = default!;
    }
#endif
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
