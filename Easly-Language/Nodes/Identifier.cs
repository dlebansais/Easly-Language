namespace BaseNode
{
    /// <summary>
    /// Represents an identifier.
    /// /Doc/Nodes/Identifier.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Identifier : Node
    {
        /// <summary>
        /// Gets or sets the identifier text.
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
    }
}
