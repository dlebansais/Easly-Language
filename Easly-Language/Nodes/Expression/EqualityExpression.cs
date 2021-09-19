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
        /// Gets or sets the left operand.
        /// </summary>
        public virtual Expression LeftExpression { get; set; } = null!;

        /// <summary>
        /// Gets or sets the comparison type.
        /// </summary>
        public virtual ComparisonType Comparison { get; set; } = ComparisonType.Equal;

        /// <summary>
        /// Gets or sets the equality type.
        /// </summary>
        public virtual EqualityType Equality { get; set; } = EqualityType.Physical;

        /// <summary>
        /// Gets or sets the right operand.
        /// </summary>
        public virtual Expression RightExpression { get; set; } = null!;
    }
}
