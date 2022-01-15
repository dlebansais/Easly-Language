namespace BaseNode;

/// <summary>
/// Represents an entity expression.
/// /Doc/Nodes/Expression/EntityExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class EntityExpression : Expression
{
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
