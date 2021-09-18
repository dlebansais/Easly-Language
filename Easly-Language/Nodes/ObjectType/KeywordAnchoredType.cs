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
        /// Gets or sets the anchor.
        /// </summary>
        public virtual Keyword Anchor { get; set; }
    }
}
