namespace BaseNode;

/// <summary>
/// Represents a class replicate.
/// /Doc/Nodes/ClassReplicate.md explains the semantic.
/// </summary>
[System.Serializable]
public class ClassReplicate : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ClassReplicate()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ReplicateName = default!;
        PatternBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ClassReplicate"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="replicateName">The class replicate name.</param>
    /// <param name="patternBlocks">The patterns.</param>
    internal ClassReplicate(Document documentation, Name replicateName, IBlockList<Pattern> patternBlocks)
        : base(documentation)
    {
        ReplicateName = replicateName;
        PatternBlocks = patternBlocks;
    }

    /// <summary>
    /// Gets or sets the class replicate name.
    /// </summary>
    public virtual Name ReplicateName { get; set; }

    /// <summary>
    /// Gets or sets the patterns.
    /// </summary>
    public virtual IBlockList<Pattern> PatternBlocks { get; set; }
}
