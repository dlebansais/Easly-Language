namespace BaseNode;

/// <summary>
/// Represents the instruction assigning value to a keyword.
/// /Doc/Nodes/Instruction/KeywordAssignmentInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class KeywordAssignmentInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public KeywordAssignmentInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Destination = default!;
        Source = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="KeywordAssignmentInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="destination">The keyword to assign.</param>
    /// <param name="source">The assigned value.</param>
    internal KeywordAssignmentInstruction(Document documentation, Keyword destination, Expression source)
        : base(documentation)
    {
        Destination = destination;
        Source = source;
    }

    /// <summary>
    /// Gets or sets the keyword to assign.
    /// </summary>
    public virtual Keyword Destination { get; set; }

    /// <summary>
    /// Gets or sets the assigned value.
    /// </summary>
    public virtual Expression Source { get; set; }
}
