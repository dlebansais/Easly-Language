namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an assertion.
    /// /Doc/Nodes/Assertion.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Assertion : Node
    {
        /// <summary>
        /// Gets or sets the assertion tag.
        /// </summary>
        public virtual IOptionalReference<Name> Tag { get; set; } = null!;

        /// <summary>
        /// Gets or sets the assertion expression.
        /// </summary>
        public virtual Expression BooleanExpression { get; set; } = null!;
    }
}
