namespace BaseNode;

/// <summary>
/// Represents an assignment instruction.
/// /Doc/Nodes/Instruction/AssignmentInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class AssignmentInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public AssignmentInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        DestinationBlocks = default!;
        Source = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="destinationBlocks">Assigned variables.</param>
    /// <param name="source">The value to assign.</param>
    internal AssignmentInstruction(Document documentation, IBlockList<QualifiedName> destinationBlocks, Expression source)
        : base(documentation)
    {
        DestinationBlocks = destinationBlocks;
        Source = source;
    }

    /// <summary>
    /// Gets or sets assigned variables.
    /// </summary>
    public virtual IBlockList<QualifiedName> DestinationBlocks { get; set; }

    /// <summary>
    /// Gets or sets the value to assign.
    /// </summary>
    public virtual Expression Source { get; set; }
}
