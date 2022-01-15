namespace BaseNode;

/// <summary>
/// Represents the 'not' conditional operator.
/// /Doc/Nodes/Expression/UnaryNotExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class UnaryNotExpression : Expression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnaryNotExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="rightExpression">The operand.</param>
    internal UnaryNotExpression(Document documentation, Expression rightExpression)
        : base(documentation)
    {
        RightExpression = rightExpression;
    }

    /// <summary>
    /// Gets or sets the operand.
    /// </summary>
    public virtual Expression RightExpression { get; set; }
}
