using Easly;

namespace BaseNode
{
    public interface IIfThenElseInstruction : IInstruction
    {
        IBlockList<IConditional, Conditional> ConditionalBlocks { get; }
        OptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class IfThenElseInstruction : Instruction, IIfThenElseInstruction
    {
        public virtual IBlockList<IConditional, Conditional> ConditionalBlocks { get; set; }
        public virtual OptionalReference<IScope> ElseInstructions { get; set; }
    }
}
