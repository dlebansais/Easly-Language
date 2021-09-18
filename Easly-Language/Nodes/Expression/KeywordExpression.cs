namespace BaseNode
{
    /// <summary>
    /// Represents a keyword expression.
    /// /Doc/Nodes/Expression/KeywordExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class KeywordExpression : Expression
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        public virtual Keyword Value { get; set; }
    }
}
