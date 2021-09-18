namespace BaseNode
{
    /// <summary>
    /// Represents an operator expression with two operands.
    /// /Doc/Nodes/Expression/BinaryOperatorExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class BinaryOperatorExpression : Expression
    {
        /// <summary>
        /// Gets or sets the left operand.
        /// </summary>
        public virtual Expression LeftExpression { get; set; } = null!;

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public virtual Identifier Operator { get; set; } = null!;

        /// <summary>
        /// Gets or sets the right operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; } = null!;
    }
}
