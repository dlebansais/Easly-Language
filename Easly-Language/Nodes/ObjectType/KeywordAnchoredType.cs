namespace BaseNode;

/// <summary>
/// Represents a type anchored to a keyword.
/// /Doc/Nodes/Type/KeywordAnchoredType.md explains the semantic.
/// </summary>
[System.Serializable]
public class KeywordAnchoredType : ObjectType
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
    public KeywordAnchoredType()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Anchor = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="KeywordAnchoredType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="anchor">The anchor.</param>
    internal KeywordAnchoredType(Document documentation, Keyword anchor)
        : base(documentation)
    {
        Anchor = anchor;
    }

    /// <summary>
    /// Gets or sets the anchor.
    /// </summary>
    public virtual Keyword Anchor { get; set; }
}
