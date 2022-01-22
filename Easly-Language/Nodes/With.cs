namespace BaseNode;

/// <summary>
/// Represents the 'with' part of the 'inspect' instruction.
/// /Doc/Nodes/With.md explains the semantic.
/// </summary>
[System.Serializable]
public class With : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public With()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        RangeBlocks = default!;
        Instructions = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="With"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="rangeBlocks">The range blocks.</param>
    /// <param name="instructions">The instructions for this case.</param>
    internal With(Document documentation, IBlockList<Range> rangeBlocks, Scope instructions)
        : base(documentation)
    {
        RangeBlocks = rangeBlocks;
        Instructions = instructions;
    }

    /// <summary>
    /// Gets or sets the range blocks.
    /// </summary>
    public virtual IBlockList<Range> RangeBlocks { get; set; }

    /// <summary>
    /// Gets or sets the instructions for this case.
    /// </summary>
    public virtual Scope Instructions { get; set; }
}
