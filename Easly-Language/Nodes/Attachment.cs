namespace BaseNode
{
    /// <summary>
    /// Represents an attachment.
    /// /Doc/Nodes/Attachment.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Attachment : Node
    {
        /// <summary>
        /// Gets or sets the blocks of attached types.
        /// </summary>
        public virtual IBlockList<ObjectType> AttachTypeBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute in case of successful attachment.
        /// </summary>
        public virtual Scope Instructions { get; set; } = null!;
    }
}
