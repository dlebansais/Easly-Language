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
        /// Initializes a new instance of the <see cref="BinaryConditionalExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="leftExpression">The left operand.</param>
        /// <param name="conditional">The conditional operator.</param>
        /// <param name="rightExpression">The right operand.</param>
        internal BinaryConditionalExpression(Document documentation, Expression leftExpression, ConditionalTypes conditional, Expression rightExpression)
            : base(documentation)
        {
            LeftExpression = leftExpression;
            Conditional = conditional;
            RightExpression = rightExpression;
        }

        /// <summary>
        /// Gets or sets the left operand.
        /// </summary>
        public virtual Expression LeftExpression { get; set; }

        /// <summary>
        /// Gets or sets the conditional operator.
        /// </summary>
        public virtual ConditionalTypes Conditional { get; set; }

        /// <summary>
        /// Gets or sets the right operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; }
    }
}
