namespace BaseNode
{
    /// <summary>
    /// Represents a clone expression.
    /// /Doc/Nodes/Expression/CloneOfExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CloneOfExpression : Expression
    {
        /// <summary>
        /// Gets or sets the close type.
        /// </summary>
        public virtual CloneType Type { get; set; } = CloneType.Shallow;

        /// <summary>
        /// Gets or sets the cloned source.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
