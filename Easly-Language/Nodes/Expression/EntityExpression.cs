namespace BaseNode;

/// <summary>
/// Represents an entity expression.
/// /Doc/Nodes/Expression/EntityExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class EntityExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public EntityExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Query = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="query">The feature to get the entity from.</param>
    internal EntityExpression(Document documentation, QualifiedName query)
        : base(documentation)
    {
        Query = query;
    }

    /// <summary>
    /// Gets or sets the feature to get the entity from.
    /// </summary>
    public virtual QualifiedName Query { get; set; }
}
