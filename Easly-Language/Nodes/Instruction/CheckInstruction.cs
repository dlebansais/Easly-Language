namespace BaseNode;

/// <summary>
/// Represents a check instruction.
/// /Doc/Nodes/Instruction/CheckInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class CheckInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public CheckInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        BooleanExpression = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="booleanExpression">The value to check.</param>
    internal CheckInstruction(Document documentation, Expression booleanExpression)
        : base(documentation)
    {
        BooleanExpression = booleanExpression;
    }

    /// <summary>
    /// Gets or sets the value to check.
    /// </summary>
    public virtual Expression BooleanExpression { get; set; }
}
