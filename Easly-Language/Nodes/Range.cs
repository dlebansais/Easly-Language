namespace BaseNode;

using Easly;

/// <summary>
/// Represents a range of constant values.
/// /Doc/Nodes/Range.md explains the semantic.
/// </summary>
[System.Serializable]
public class Range : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Range"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="leftExpression">The single constant value, or the left side of the range in case of multiple values.</param>
    /// <param name="rightExpression">The right side of the range in case of multiple values.</param>
    internal Range(Document documentation, Expression leftExpression, IOptionalReference<Expression> rightExpression)
        : base(documentation)
    {
        LeftExpression = leftExpression;
        RightExpression = rightExpression;
    }

    /// <summary>
    /// Gets or sets the single constant value, or the left side of the range in case of multiple values.
    /// </summary>
    public virtual Expression LeftExpression { get; set; }

    /// <summary>
    /// Gets or sets the right side of the range in case of multiple values.
    /// </summary>
    public virtual IOptionalReference<Expression> RightExpression { get; set; }
}
