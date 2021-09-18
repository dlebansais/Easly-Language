namespace BaseNode
{
    /// <summary>
    /// Represents the tag pointing to an assertion.
    /// /Doc/Nodes/Expression/AssertionTagExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AssertionTagExpression : Expression
    {
        /// <summary>
        /// Gets or sets the assertion tag.
        /// </summary>
        public virtual Identifier TagIdentifier { get; set; } = null!;
    }
}
