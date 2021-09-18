namespace BaseNode
{
    /// <summary>
    /// Represents the pattern in a replicated block.
    /// /Doc/Nodes/Pattern.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Pattern : Node
    {
        /// <summary>
        /// Gets or sets the pattern text.
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
    }
}
