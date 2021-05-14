#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
