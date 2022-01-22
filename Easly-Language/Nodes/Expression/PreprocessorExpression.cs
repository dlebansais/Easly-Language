namespace BaseNode;

/// <summary>
/// Represents a preprocessor expression.
/// /Doc/Nodes/Expression/PreprocessorExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class PreprocessorExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public PreprocessorExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Value = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="PreprocessorExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="value">The preprocessor to get the value from.</param>
    internal PreprocessorExpression(Document documentation, PreprocessorMacro value)
        : base(documentation)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the preprocessor to get the value from.
    /// </summary>
    public virtual PreprocessorMacro Value { get; set; }
}
