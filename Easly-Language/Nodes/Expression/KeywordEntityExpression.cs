namespace BaseNode;

/// <summary>
/// Represents an entity expression for a keyword.
/// /Doc/Nodes/Expression/KeywordEntityExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class KeywordEntityExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public KeywordEntityExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Value = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="KeywordEntityExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="value">The keyword.</param>
    internal KeywordEntityExpression(Document documentation, Keyword value)
        : base(documentation)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the keyword.
    /// </summary>
    public virtual Keyword Value { get; set; }
}
