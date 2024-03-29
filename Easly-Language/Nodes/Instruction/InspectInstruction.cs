namespace BaseNode;

using Easly;

/// <summary>
/// Represents an expression inspection instruction.
/// /Doc/Nodes/Instruction/InspectInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class InspectInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public InspectInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Source = default!;
        WithBlocks = default!;
        ElseInstructions = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="InspectInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="source">The inspected expression.</param>
    /// <param name="withBlocks">The list of cases.</param>
    /// <param name="elseInstructions">Instructions to execute if none of the cases matched.</param>
    internal InspectInstruction(Document documentation, Expression source, IBlockList<With> withBlocks, IOptionalReference<Scope> elseInstructions)
        : base(documentation)
    {
        Source = source;
        WithBlocks = withBlocks;
        ElseInstructions = elseInstructions;
    }

    /// <summary>
    /// Gets or sets the inspected expression.
    /// </summary>
    public virtual Expression Source { get; set; }

    /// <summary>
    /// Gets or sets the list of cases.
    /// </summary>
    public virtual IBlockList<With> WithBlocks { get; set; }

    /// <summary>
    /// Gets or sets instructions to execute if none of the cases matched.
    /// </summary>
    public virtual IOptionalReference<Scope> ElseInstructions { get; set; }
}
