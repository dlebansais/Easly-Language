namespace BaseNode;

/// <summary>
/// Represents the 'with' part of the 'inspect' instruction.
/// /Doc/Nodes/With.md explains the semantic.
/// </summary>
[System.Serializable]
public class With : Node
{
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
