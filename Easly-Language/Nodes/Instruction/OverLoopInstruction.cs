namespace BaseNode;

using Easly;

/// <summary>
/// Represents the instruction looping over collections.
/// /Doc/Nodes/Instruction/OverLoopInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class OverLoopInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public OverLoopInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        OverList = default!;
        IndexerBlocks = default!;
        Iteration = default!;
        LoopInstructions = default!;
        ExitEntityName = default!;
        InvariantBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="OverLoopInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="overList">The collection(s) to loop over.</param>
    /// <param name="indexerBlocks">The created indexes for each collection.</param>
    /// <param name="iteration">Whether the iteration is parallel or recursive.</param>
    /// <param name="loopInstructions">Instructions within the loop.</param>
    /// <param name="exitEntityName">The identifier of a variable to check and exit if true.</param>
    /// <param name="invariantBlocks">The loop invariants.</param>
    internal OverLoopInstruction(Document documentation, Expression overList, IBlockList<Name> indexerBlocks, IterationType iteration, Scope loopInstructions, IOptionalReference<Identifier> exitEntityName, IBlockList<Assertion> invariantBlocks)
        : base(documentation)
    {
        OverList = overList;
        IndexerBlocks = indexerBlocks;
        Iteration = iteration;
        LoopInstructions = loopInstructions;
        ExitEntityName = exitEntityName;
        InvariantBlocks = invariantBlocks;
    }

    /// <summary>
    /// Gets or sets the collection(s) to loop over.
    /// </summary>
    public virtual Expression OverList { get; set; }

    /// <summary>
    /// Gets or sets the created indexes for each collection.
    /// </summary>
    public virtual IBlockList<Name> IndexerBlocks { get; set; }

    /// <summary>
    /// Gets or sets whether the iteration is parallel or recursive.
    /// </summary>
    public virtual IterationType Iteration { get; set; }

    /// <summary>
    /// Gets or sets instructions within the loop.
    /// </summary>
    public virtual Scope LoopInstructions { get; set; }

    /// <summary>
    /// Gets or sets the identifier of a variable to check and exit if true.
    /// </summary>
    public virtual IOptionalReference<Identifier> ExitEntityName { get; set; }

    /// <summary>
    /// Gets or sets the loop invariants.
    /// </summary>
    public virtual IBlockList<Assertion> InvariantBlocks { get; set; }
}
