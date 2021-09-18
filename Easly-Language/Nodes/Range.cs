namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a range of constant values.
    /// /Doc/Nodes/Range.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Range : Node
    {
        /// <summary>
        /// Gets or sets the single constant value, or the left side of the range in case of multiple values.
        /// </summary>
        public virtual Expression LeftExpression { get; set; } = null!;

        /// <summary>
        /// Gets or sets the right side of the range in case of multiple values.
        /// </summary>
        public virtual IOptionalReference<Expression> RightExpression { get; set; } = null!;
    }
}
