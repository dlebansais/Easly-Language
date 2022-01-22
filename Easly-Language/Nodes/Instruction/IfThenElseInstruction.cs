namespace BaseNode;

using Easly;

/// <summary>
/// Represents a branching instruction.
/// /Doc/Nodes/Instruction/IfThenElseInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class IfThenElseInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public IfThenElseInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ConditionalBlocks = default!;
        ElseInstructions = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="IfThenElseInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="conditionalBlocks">The list of conditionals.</param>
    /// <param name="elseInstructions">Instructions to execute if all conditionals were false.</param>
    internal IfThenElseInstruction(Document documentation, IBlockList<Conditional> conditionalBlocks, IOptionalReference<Scope> elseInstructions)
        : base(documentation)
    {
        ConditionalBlocks = conditionalBlocks;
        ElseInstructions = elseInstructions;
    }

    /// <summary>
    /// Gets or sets the list of conditionals.
    /// </summary>
    public virtual IBlockList<Conditional> ConditionalBlocks { get; set; }

    /// <summary>
    /// Gets or sets instructions to execute if all conditionals were false.
    /// </summary>
    public virtual IOptionalReference<Scope> ElseInstructions { get; set; }
}
