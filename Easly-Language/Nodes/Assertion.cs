namespace BaseNode;

using Easly;

/// <summary>
/// Represents an assertion.
/// /Doc/Nodes/Assertion.md explains the semantic.
/// </summary>
[System.Serializable]
public class Assertion : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Assertion"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="tag">The assertion tag.</param>
    /// <param name="booleanExpression">The assertion expression.</param>
    internal Assertion(Document documentation, IOptionalReference<Name> tag, Expression booleanExpression)
        : base(documentation)
    {
        Tag = tag;
        BooleanExpression = booleanExpression;
    }

    /// <summary>
    /// Gets or sets the assertion tag.
    /// </summary>
    public virtual IOptionalReference<Name> Tag { get; set; }

    /// <summary>
    /// Gets or sets the assertion expression.
    /// </summary>
    public virtual Expression BooleanExpression { get; set; }
}
