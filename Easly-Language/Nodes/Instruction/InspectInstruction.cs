#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
