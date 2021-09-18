namespace BaseNode
{
    /// <summary>
    /// Represents the definition of a name.
    /// /Doc/Nodes/Name.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Name : Node
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Text { get; set; } = string.Empty;
    }
}
