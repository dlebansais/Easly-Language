namespace BaseNode;

/// <summary>
/// Represents the pattern in a replicated block.
/// /Doc/Nodes/Pattern.md explains the semantic.
/// </summary>
[System.Serializable]
public class Pattern : Node
{
    /// <summary>
    /// Gets the default <see cref="Pattern"/> object.
    /// </summary>
    public static new Pattern Default { get; } = new();

#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Pattern()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Text = string.Empty;
    }
#else
    /// <summary>
    /// Initializes a new instance of the <see cref="Pattern"/> class.
    /// </summary>
    protected Pattern()
    {
        Text = string.Empty;
    }
#endif

    /// <summary>
    /// Initializes a new instance of the <see cref="Pattern"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="text">The pattern text.</param>
    internal Pattern(Document documentation, string text)
        : base(documentation)
    {
        Text = text;
    }

    /// <summary>
    /// Gets or sets the pattern text.
    /// </summary>
    public virtual string Text { get; set; }
}
