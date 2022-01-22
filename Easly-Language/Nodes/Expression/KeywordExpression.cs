namespace BaseNode;

/// <summary>
/// Represents a keyword expression.
/// /Doc/Nodes/Expression/KeywordExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class KeywordExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public KeywordExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Value = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="KeywordExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="value">The keyword.</param>
    internal KeywordExpression(Document documentation, Keyword value)
        : base(documentation)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the keyword.
    /// </summary>
    public virtual Keyword Value { get; set; }
}
