#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IInspectInstruction : IInstruction
    {
        IExpression Source { get; }
        IBlockList<IWith, With> WithBlocks { get; }
        IOptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class InspectInstruction : Instruction, IInspectInstruction
    {
        public virtual IExpression Source { get; set; }
        public virtual IBlockList<IWith, With> WithBlocks { get; set; }
        public virtual IOptionalReference<IScope> ElseInstructions { get; set; }
    }
}
