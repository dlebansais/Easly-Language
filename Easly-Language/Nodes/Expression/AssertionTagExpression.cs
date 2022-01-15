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
        /// Initializes a new instance of the <see cref="AssertionTagExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="tagIdentifier">The assertion tag.</param>
        internal AssertionTagExpression(Document documentation, Identifier tagIdentifier)
            : base(documentation)
        {
            TagIdentifier = tagIdentifier;
        }

        /// <summary>
        /// Gets or sets the assertion tag.
        /// </summary>
        public virtual Identifier TagIdentifier { get; set; }
    }
}
