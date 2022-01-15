namespace BaseNode
{
    /// <summary>
    /// Represents generic documentation.
    /// /Doc/Nodes/Document.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Document
    {
        /// <summary>
        /// Gets or sets the text comment.
        /// </summary>
        public virtual string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a unique ID.
        /// </summary>
        public virtual System.Guid Uuid { get; set; } = System.Guid.Empty;
    }
}
