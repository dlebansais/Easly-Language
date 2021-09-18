namespace BaseNode
{
    /// <summary>
    /// Represents the 'not' conditional operator.
    /// /Doc/Nodes/Expression/UnaryNotExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class UnaryNotExpression : Expression
    {
        /// <summary>
        /// Gets or sets the operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; } = null!;
    }
}
