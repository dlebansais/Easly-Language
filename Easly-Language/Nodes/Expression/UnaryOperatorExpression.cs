namespace BaseNode
{
    /// <summary>
    /// Represents an operator expression with one operand.
    /// /Doc/Nodes/Expression/UnaryOperatorExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class UnaryOperatorExpression : Expression
    {
        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public virtual Identifier Operator { get; set; } = null!;

        /// <summary>
        /// Gets or sets the operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; } = null!;
    }
}
