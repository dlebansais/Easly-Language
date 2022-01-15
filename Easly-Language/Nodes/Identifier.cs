namespace BaseNode;

/// <summary>
/// Represents an identifier.
/// /Doc/Nodes/Identifier.md explains the semantic.
/// </summary>
[System.Serializable]
public class Identifier : Node
{
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
