namespace BaseNode
{
    /// <summary>
    /// Represents an equality expression.
    /// /Doc/Nodes/Expression/EqualityExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class EqualityExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="leftExpression">The left operand.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <param name="equality">The equality type.</param>
        /// <param name="rightExpression">The right operand.</param>
        internal EqualityExpression(Document documentation, Expression leftExpression, ComparisonType comparison, EqualityType equality, Expression rightExpression)
            : base(documentation)
        {
            LeftExpression = leftExpression;
            Comparison = comparison;
            Equality = equality;
            RightExpression = rightExpression;
        }

        /// <summary>
        /// Gets or sets the left operand.
        /// </summary>
        public virtual Expression LeftExpression { get; set; }

        /// <summary>
        /// Gets or sets the comparison type.
        /// </summary>
        public virtual ComparisonType Comparison { get; set; }

        /// <summary>
        /// Gets or sets the equality type.
        /// </summary>
        public virtual EqualityType Equality { get; set; }

        /// <summary>
        /// Gets or sets the right operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; }
    }
}
