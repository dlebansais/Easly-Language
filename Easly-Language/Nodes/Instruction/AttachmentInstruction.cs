#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

    public interface IAttachmentInstruction : IInstruction
    {
        IExpression Source { get; }
        IBlockList<IName, Name> EntityNameBlocks { get; }
        IBlockList<IAttachment, Attachment> AttachmentBlocks { get; }
        IOptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class AttachmentInstruction : Instruction, IAttachmentInstruction
    {
        public virtual IExpression Source { get; set; }
        public virtual IBlockList<IName, Name> EntityNameBlocks { get; set; }
        public virtual IBlockList<IAttachment, Attachment> AttachmentBlocks { get; set; }
        public virtual IOptionalReference<IScope> ElseInstructions { get; set; }
    }
}
