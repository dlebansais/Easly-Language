namespace BaseNode;

/// <summary>
/// Represents an index assignment instruction.
/// /Doc/Nodes/Instruction/IndexAssignmentInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class IndexAssignmentInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public IndexAssignmentInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Destination = default!;
        ArgumentBlocks = default!;
        Source = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexAssignmentInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="destination">The assigned variable.</param>
    /// <param name="argumentBlocks">The call arguments.</param>
    /// <param name="source">The index in the destination.</param>
    internal IndexAssignmentInstruction(Document documentation, QualifiedName destination, IBlockList<Argument> argumentBlocks, Expression source)
        : base(documentation)
    {
        Destination = destination;
        ArgumentBlocks = argumentBlocks;
        Source = source;
    }

    /// <summary>
    /// Gets or sets the assigned variable.
    /// </summary>
    public virtual QualifiedName Destination { get; set; }

    /// <summary>
    /// Gets or sets the call arguments.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }

    /// <summary>
    /// Gets or sets the index in the destination.
    /// </summary>
    public virtual Expression Source { get; set; }
}
