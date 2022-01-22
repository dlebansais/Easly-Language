namespace BaseNode;

/// <summary>
/// Represents a number constant expression.
/// /Doc/Nodes/Expression/ManifestNumberExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class ManifestNumberExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ManifestNumberExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Text = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ManifestNumberExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="text">The number text representation.</param>
    internal ManifestNumberExpression(Document documentation, string text)
        : base(documentation)
    {
        Text = text;
    }

    /// <summary>
    /// Gets or sets the number text representation.
    /// </summary>
    public virtual string Text { get; set; }
}
