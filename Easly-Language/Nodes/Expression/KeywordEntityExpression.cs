namespace BaseNode
{
    /// <summary>
    /// Represents an entity expression for a keyword.
    /// /Doc/Nodes/Expression/KeywordEntityExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class KeywordEntityExpression : Expression
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        public virtual Keyword Value { get; set; } = Keyword.True;
    }
}
