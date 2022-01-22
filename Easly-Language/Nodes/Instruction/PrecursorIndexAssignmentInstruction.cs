namespace BaseNode;

using Easly;

/// <summary>
/// Represents the assigment of the precursor of an indexer.
/// /Doc/Nodes/Instruction/PrecursorIndexAssignmentInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class PrecursorIndexAssignmentInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public PrecursorIndexAssignmentInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        AncestorType = default!;
        ArgumentBlocks = default!;
        Source = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="PrecursorIndexAssignmentInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="ancestorType">The type where to find the precursor.</param>
    /// <param name="argumentBlocks">The call arguments.</param>
    /// <param name="source">The value to assign.</param>
    internal PrecursorIndexAssignmentInstruction(Document documentation, IOptionalReference<ObjectType> ancestorType, IBlockList<Argument> argumentBlocks, Expression source)
        : base(documentation)
    {
        AncestorType = ancestorType;
        ArgumentBlocks = argumentBlocks;
        Source = source;
    }

    /// <summary>
    /// Gets or sets the type where to find the precursor.
    /// </summary>
    public virtual IOptionalReference<ObjectType> AncestorType { get; set; }

    /// <summary>
    /// Gets or sets the call arguments.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }

    /// <summary>
    /// Gets or sets the value to assign.
    /// </summary>
    public virtual Expression Source { get; set; }
}
