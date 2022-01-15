namespace BaseNode
{
    /// <summary>
    /// Represents a type anchored to a keyword.
    /// /Doc/Nodes/Type/KeywordAnchoredType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class KeywordAnchoredType : ObjectType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordAnchoredType"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="anchor">The anchor.</param>
        internal KeywordAnchoredType(Document documentation, Keyword anchor)
            : base(documentation)
        {
            Anchor = anchor;
        }

        /// <summary>
        /// Gets or sets the anchor.
        /// </summary>
        public virtual Keyword Anchor { get; set; }
    }
}
