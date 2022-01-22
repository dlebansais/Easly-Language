namespace BaseNode;

/// <summary>
/// Represents the tag pointing to an assertion.
/// /Doc/Nodes/Expression/AssertionTagExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class AssertionTagExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public AssertionTagExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        TagIdentifier = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionTagExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="tagIdentifier">The assertion tag.</param>
    internal AssertionTagExpression(Document documentation, Identifier tagIdentifier)
        : base(documentation)
    {
        TagIdentifier = tagIdentifier;
    }

    /// <summary>
    /// Gets or sets the assertion tag.
    /// </summary>
    public virtual Identifier TagIdentifier { get; set; }
}
