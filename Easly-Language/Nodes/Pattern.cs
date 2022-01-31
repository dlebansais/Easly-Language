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

    /// <summary>
    /// Initializes a new instance of the <see cref="Pattern"/> class.
    /// </summary>
    private Pattern()
    {
        Text = string.Empty;
    }

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
