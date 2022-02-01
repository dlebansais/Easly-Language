namespace BaseNode;

using System;

/// <summary>
/// Represents generic documentation.
/// /Doc/Nodes/Document.md explains the semantic.
/// </summary>
[Serializable]
public class Document
{
    /// <summary>
    /// Gets the default <see cref="Document"/> object.
    /// </summary>
    public static Document Default { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// </summary>
    private Document()
    {
        Comment = string.Empty;
        Uuid = Guid.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// </summary>
    /// <param name="comment">The text comment.</param>
    /// <param name="uuid">The unique ID.</param>
    internal Document(string comment, Guid uuid)
    {
        Comment = comment;
        Uuid = uuid;
    }

    /// <summary>
    /// Gets or sets the text comment.
    /// </summary>
    public virtual string Comment { get; set; }

    /// <summary>
    /// Gets or sets the unique ID.
    /// </summary>
    public virtual Guid Uuid { get; set; }
}
