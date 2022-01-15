namespace BaseNode;

/// <summary>
/// Represents a preprocessor expression.
/// /Doc/Nodes/Expression/PreprocessorExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class PreprocessorExpression : Expression
{
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
