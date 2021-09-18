namespace BaseNode
{
    /// <summary>
    /// Represents a string constant expression.
    /// /Doc/Nodes/Expression/ManifestStringExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ManifestStringExpression : Expression
    {
        /// <summary>
        /// Gets or sets the string.
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
    }
}
