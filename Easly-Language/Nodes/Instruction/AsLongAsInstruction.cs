namespace BaseNode;

using Easly;

/// <summary>
/// Represents the 'as long as' instruction.
/// /Doc/Nodes/Instruction/AsLongAsInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class AsLongAsInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public AsLongAsInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ContinueCondition = default!;
        ContinuationBlocks = default!;
        ElseInstructions = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="AsLongAsInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="continueCondition">The ncondition evaluated to continue.</param>
    /// <param name="continuationBlocks">The list of continuations.</param>
    /// <param name="elseInstructions">Instructions to execute when we don't continue.</param>
    internal AsLongAsInstruction(Document documentation, Expression continueCondition, IBlockList<Continuation> continuationBlocks, IOptionalReference<Scope> elseInstructions)
        : base(documentation)
    {
        ContinueCondition = continueCondition;
        ContinuationBlocks = continuationBlocks;
        ElseInstructions = elseInstructions;
    }

    /// <summary>
    /// Gets or sets the condition evaluated to continue.
    /// </summary>
    public virtual Expression ContinueCondition { get; set; }

    /// <summary>
    /// Gets or sets the list of continuations.
    /// </summary>
    public virtual IBlockList<Continuation> ContinuationBlocks { get; set; }

    /// <summary>
    /// Gets or sets instructions to execute when we don't continue.
    /// </summary>
    public virtual IOptionalReference<Scope> ElseInstructions { get; set; }
}
