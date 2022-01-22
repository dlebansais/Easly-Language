namespace BaseNode;

/// <summary>
/// Represents the expression result of another expression.
/// /Doc/Nodes/Expression/ResultOfExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class ResultOfExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ResultOfExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Source = default!;
    }
#endif
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
