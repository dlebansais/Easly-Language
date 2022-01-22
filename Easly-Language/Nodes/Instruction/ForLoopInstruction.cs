namespace BaseNode;

using Easly;

/// <summary>
/// Represents a loop instruction.
/// /Doc/Nodes/Instruction/ForLoopInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class ForLoopInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ForLoopInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        EntityDeclarationBlocks = default!;
        InitInstructionBlocks = default!;
        WhileCondition = default!;
        LoopInstructionBlocks = default!;
        IterationInstructionBlocks = default!;
        InvariantBlocks = default!;
        Variant = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ForLoopInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityDeclarationBlocks">The loop variables.</param>
    /// <param name="initInstructionBlocks">Instructions to initialize the loop.</param>
    /// <param name="whileCondition">The expression to continue.</param>
    /// <param name="loopInstructionBlocks">Instructions within the loop.</param>
    /// <param name="iterationInstructionBlocks">Instructions for the next iteration of the loop.</param>
    /// <param name="invariantBlocks">The loop invariants.</param>
    /// <param name="variant">The loop variant.</param>
    internal ForLoopInstruction(Document documentation, IBlockList<EntityDeclaration> entityDeclarationBlocks, IBlockList<Instruction> initInstructionBlocks, Expression whileCondition, IBlockList<Instruction> loopInstructionBlocks, IBlockList<Instruction> iterationInstructionBlocks, IBlockList<Assertion> invariantBlocks, IOptionalReference<Expression> variant)
        : base(documentation)
    {
        EntityDeclarationBlocks = entityDeclarationBlocks;
        InitInstructionBlocks = initInstructionBlocks;
        WhileCondition = whileCondition;
        LoopInstructionBlocks = loopInstructionBlocks;
        IterationInstructionBlocks = iterationInstructionBlocks;
        InvariantBlocks = invariantBlocks;
        Variant = variant;
    }

    /// <summary>
    /// Gets or sets the loop variables.
    /// </summary>
    public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; }

    /// <summary>
    /// Gets or sets instructions to initialize the loop.
    /// </summary>
    public virtual IBlockList<Instruction> InitInstructionBlocks { get; set; }

    /// <summary>
    /// Gets or sets the expression to continue.
    /// </summary>
    public virtual Expression WhileCondition { get; set; }

    /// <summary>
    /// Gets or sets instructions within the loop.
    /// </summary>
    public virtual IBlockList<Instruction> LoopInstructionBlocks { get; set; }

    /// <summary>
    /// Gets or sets instructions for the next iteration of the loop.
    /// </summary>
    public virtual IBlockList<Instruction> IterationInstructionBlocks { get; set; }

    /// <summary>
    /// Gets or sets the loop invariants.
    /// </summary>
    public virtual IBlockList<Assertion> InvariantBlocks { get; set; }

    /// <summary>
    /// Gets or sets the loop variant.
    /// </summary>
    public virtual IOptionalReference<Expression> Variant { get; set; }
}
