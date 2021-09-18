namespace BaseNode
{
    /// <summary>
    /// Represents a conditional expression with two operands.
    /// /Doc/Nodes/Expression/BinaryConditionalExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class BinaryConditionalExpression : Expression
    {
        /// <summary>
        /// Gets or sets the left operand.
        /// </summary>
        public virtual Expression LeftExpression { get; set; } = null!;

        /// <summary>
        /// Gets or sets the conditional operator.
        /// </summary>
        public virtual ConditionalTypes Conditional { get; set; }

        /// <summary>
        /// Gets or sets the right operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; } = null!;
    }
}
