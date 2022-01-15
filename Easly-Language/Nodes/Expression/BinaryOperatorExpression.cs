namespace BaseNode;

/// <summary>
/// Represents an operator expression with two operands.
/// /Doc/Nodes/Expression/BinaryOperatorExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class BinaryOperatorExpression : Expression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryOperatorExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="leftExpression">The left operand.</param>
    /// <param name="operator">The operator.</param>
    /// <param name="rightExpression">The right operand.</param>
    internal BinaryOperatorExpression(Document documentation, Expression leftExpression, Identifier @operator, Expression rightExpression)
        : base(documentation)
    {
        LeftExpression = leftExpression;
        Operator = @operator;
        RightExpression = rightExpression;
    }

    /// <summary>
    /// Gets or sets the left operand.
    /// </summary>
    public virtual Expression LeftExpression { get; set; }

    /// <summary>
    /// Gets or sets the operator.
    /// </summary>
    public virtual Identifier Operator { get; set; }

    /// <summary>
    /// Gets or sets the right operand.
    /// </summary>
    public virtual Expression RightExpression { get; set; }
}
