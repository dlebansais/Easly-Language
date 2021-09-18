namespace BaseNode
{
    /// <summary>
    /// Represents the expression result of another expression.
    /// /Doc/Nodes/Expression/ResultOfExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ResultOfExpression : Expression
    {
        /// <summary>
        /// Gets or sets the source expression.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
