namespace BaseNode
{
    /// <summary>
    /// Represents a number constant expression.
    /// /Doc/Nodes/Expression/ManifestNumberExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ManifestNumberExpression : Expression
    {
        /// <summary>
        /// Gets or sets the number text representation.
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
    }
}
