namespace BaseNode;

/// <summary>
/// Represents a character constant expression.
/// /Doc/Nodes/Expression/ManifestCharacterExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class ManifestCharacterExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ManifestCharacterExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Text = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ManifestCharacterExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="text">The character.</param>
    internal ManifestCharacterExpression(Document documentation, string text)
        : base(documentation)
    {
        Text = text;
    }

    /// <summary>
    /// Gets or sets the character.
    /// </summary>
    public virtual string Text { get; set; }
}
