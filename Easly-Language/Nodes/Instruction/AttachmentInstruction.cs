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
        /// Initializes a new instance of the <see cref="AttachmentInstruction"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="source">The value to attach.</param>
        /// <param name="entityNameBlocks">The list of variables created upon attachment.</param>
        /// <param name="attachmentBlocks">The list of possible attachments.</param>
        /// <param name="elseInstructions">Instructions to execute when not attaching.</param>
        internal AttachmentInstruction(Document documentation, Expression source, IBlockList<Name> entityNameBlocks, IBlockList<Attachment> attachmentBlocks, IOptionalReference<Scope> elseInstructions)
            : base(documentation)
        {
            Source = source;
            EntityNameBlocks = entityNameBlocks;
            AttachmentBlocks = attachmentBlocks;
            ElseInstructions = elseInstructions;
        }

        /// <summary>
        /// Gets or sets the value to attach.
        /// </summary>
        public virtual Expression Source { get; set; }

        /// <summary>
        /// Gets or sets the list of variables created upon attachment.
        /// </summary>
        public virtual IBlockList<Name> EntityNameBlocks { get; set; }

        /// <summary>
        /// Gets or sets the list of possible attachments.
        /// </summary>
        public virtual IBlockList<Attachment> AttachmentBlocks { get; set; }

        /// <summary>
        /// Gets or sets instructions to execute when not attaching.
        /// </summary>
        public virtual IOptionalReference<Scope> ElseInstructions { get; set; }
    }
}
