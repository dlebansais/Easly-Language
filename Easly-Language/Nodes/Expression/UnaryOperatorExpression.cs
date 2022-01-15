namespace BaseNode;

/// <summary>
/// Represents an operator expression with one operand.
/// /Doc/Nodes/Expression/UnaryOperatorExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class UnaryOperatorExpression : Expression
{
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
