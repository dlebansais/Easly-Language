namespace BaseNode;

/// <summary>
/// Represents an operator expression with one operand.
/// /Doc/Nodes/Expression/UnaryOperatorExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class UnaryOperatorExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public UnaryOperatorExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Operator = default!;
        RightExpression = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryOperatorExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="operator">The operator.</param>
    /// <param name="rightExpression">The operand.</param>
    internal UnaryOperatorExpression(Document documentation, Identifier @operator, Expression rightExpression)
        : base(documentation)
    {
        Operator = @operator;
        RightExpression = rightExpression;
    }

    /// <summary>
    /// Gets or sets the operator.
    /// </summary>
    public virtual Identifier Operator { get; set; }

    /// <summary>
    /// Gets or sets the operand.
    /// </summary>
    public virtual Expression RightExpression { get; set; }
}
