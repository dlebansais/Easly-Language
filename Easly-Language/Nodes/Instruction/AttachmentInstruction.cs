namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an attachment instruction.
    /// /Doc/Nodes/Instruction/AttachmentInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AttachmentInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the value to attach.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of variables created upon attachment.
        /// </summary>
        public virtual IBlockList<Name> EntityNameBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of possible attachments.
        /// </summary>
        public virtual IBlockList<Attachment> AttachmentBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute when not attaching.
        /// </summary>
        public virtual IOptionalReference<Scope> ElseInstructions { get; set; } = null!;
    }
}
