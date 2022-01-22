namespace BaseNode;

/// <summary>
/// Represents a string constant expression.
/// /Doc/Nodes/Expression/ManifestStringExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class ManifestStringExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ManifestStringExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Text = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ManifestStringExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="text">The string.</param>
    internal ManifestStringExpression(Document documentation, string text)
        : base(documentation)
    {
        Text = text;
    }

    /// <summary>
    /// Gets or sets the string.
    /// </summary>
    public virtual string Text { get; set; }
}
