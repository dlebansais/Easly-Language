#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

    public interface IIfThenElseInstruction : IInstruction
    {
        IBlockList<IConditional, Conditional> ConditionalBlocks { get; }
        IOptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class IfThenElseInstruction : Instruction, IIfThenElseInstruction
    {
        public virtual IBlockList<IConditional, Conditional> ConditionalBlocks { get; set; }
        public virtual IOptionalReference<IScope> ElseInstructions { get; set; }
    }
}
