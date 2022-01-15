namespace BaseNode;

/// <summary>
/// Represents the expression result of another expression.
/// /Doc/Nodes/Expression/ResultOfExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class ResultOfExpression : Expression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultOfExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="source">The path to the queried feature.</param>
    internal ResultOfExpression(Document documentation, Expression source)
        : base(documentation)
    {
        Source = source;
    }

    /// <summary>
    /// Gets or sets the source expression.
    /// </summary>
    public virtual Expression Source { get; set; }
}
