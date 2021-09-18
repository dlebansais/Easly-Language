namespace BaseNode
{
    /// <summary>
    /// Represents an entity expression.
    /// /Doc/Nodes/Expression/EntityExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class EntityExpression : Expression
    {
        /// <summary>
        /// Gets or sets the feature to get the entity from.
        /// </summary>
        public virtual QualifiedName Query { get; set; } = null!;
    }
}
