using Easly;

namespace BaseNode
{
    public interface IAttachmentInstruction : IInstruction
    {
        IExpression Source { get; }
        IBlockList<IName, Name> EntityNameBlocks { get; }
        IBlockList<IAttachment, Attachment> AttachmentBlocks { get; }
        OptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class AttachmentInstruction : Instruction, IAttachmentInstruction
    {
        public virtual IExpression Source { get; set; }
        public virtual IBlockList<IName, Name> EntityNameBlocks { get; set; }
        public virtual IBlockList<IAttachment, Attachment> AttachmentBlocks { get; set; }
        public virtual OptionalReference<IScope> ElseInstructions { get; set; }
    }
}