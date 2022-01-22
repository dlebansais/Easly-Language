namespace BaseNode;

/// <summary>
/// Represents a conditional in a 'if' instruction.
/// /Doc/Nodes/Conditional.md explains the semantic.
/// </summary>
[System.Serializable]
public class Conditional : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Conditional()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        BooleanExpression = default!;
        Instructions = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Conditional"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="booleanExpression">The command parameters.</param>
    /// <param name="instructions">Whether the command accepts extra parameters.</param>
    internal Conditional(Document documentation, Expression booleanExpression, Scope instructions)
        : base(documentation)
    {
        BooleanExpression = booleanExpression;
        Instructions = instructions;
    }

    /// <summary>
    /// Gets or sets the condition.
    /// </summary>
    public virtual Expression BooleanExpression { get; set; }

    /// <summary>
    /// Gets or sets instructions to execute if the condition is true.
    /// </summary>
    public virtual Scope Instructions { get; set; }
}
