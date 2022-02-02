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

#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Document()
#pragma warning restore SA1600 // Elements should be documented
    {
        Comment = string.Empty;
        Uuid = Guid.Empty;
    }
#else
    /// <summary>
    /// Initializes a new instance of the <see cref="Document"/> class.
    /// </summary>
    protected Document()
    {
        Comment = string.Empty;
        Uuid = Guid.Empty;
    }
#endif

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
