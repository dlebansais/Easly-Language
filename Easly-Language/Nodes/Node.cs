namespace BaseNode
{
    /// <summary>
    /// Represents any node.
    /// </summary>
    [System.Serializable]
    public abstract class Node
    {
        /// <summary>
        /// Gets or sets the node documentation.
        /// </summary>
        public virtual Document Documentation { get; set; } = null!;
    }
}
