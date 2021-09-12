#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class AttachmentInstruction : Instruction
    {
        public virtual Expression Source { get; set; }
        public virtual BlockList<Name> EntityNameBlocks { get; set; }
        public virtual BlockList<Attachment> AttachmentBlocks { get; set; }
        public virtual OptionalReference<Scope> ElseInstructions { get; set; }
    }
}
