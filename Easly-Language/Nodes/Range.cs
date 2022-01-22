namespace BaseNode;

using Easly;

/// <summary>
/// Represents a range of constant values.
/// /Doc/Nodes/Range.md explains the semantic.
/// </summary>
[System.Serializable]
public class Range : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Range()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        LeftExpression = default!;
        RightExpression = default!;
    }
#endif
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
