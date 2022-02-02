namespace BaseNode;

/// <summary>
/// Represents an identifier.
/// /Doc/Nodes/Identifier.md explains the semantic.
/// </summary>
[System.Serializable]
public class Identifier : Node
{
    /// <summary>
    /// Gets the default <see cref="Identifier"/> object.
    /// </summary>
    public static new Identifier Default { get; } = new();

#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Identifier()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Text = string.Empty;
    }
#else
    /// <summary>
    /// Initializes a new instance of the <see cref="Identifier"/> class.
    /// </summary>
    protected Identifier()
    {
        Text = string.Empty;
    }
#endif

    /// <summary>
    /// Initializes a new instance of the <see cref="Identifier"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="text">The identifier text.</param>
    internal Identifier(Document documentation, string text)
        : base(documentation)
    {
        Text = text;
    }

    /// <summary>
    /// Gets or sets the identifier text.
    /// </summary>
    public virtual string Text { get; set; }
}
