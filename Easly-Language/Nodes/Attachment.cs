namespace BaseNode;

/// <summary>
/// Represents an attachment.
/// /Doc/Nodes/Attachment.md explains the semantic.
/// </summary>
[System.Serializable]
public class Attachment : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Attachment"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="attachTypeBlocks">The blocks of attached types.</param>
    /// <param name="instructions">Instructions to execute in case of successful attachment.</param>
    internal Attachment(Document documentation, IBlockList<ObjectType> attachTypeBlocks, Scope instructions)
        : base(documentation)
    {
        AttachTypeBlocks = attachTypeBlocks;
        Instructions = instructions;
    }

    /// <summary>
    /// Gets or sets the blocks of attached types.
    /// </summary>
    public virtual IBlockList<ObjectType> AttachTypeBlocks { get; set; }

    /// <summary>
    /// Gets or sets instructions to execute in case of successful attachment.
    /// </summary>
    public virtual Scope Instructions { get; set; }
}
