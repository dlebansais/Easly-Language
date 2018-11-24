using Easly;

namespace BaseNode
{
    public interface IInspectInstruction : IInstruction
    {
        IExpression Source { get; }
        IBlockList<IWith, With> WithBlocks { get; }
        OptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class InspectInstruction : Instruction, IInspectInstruction
    {
        public virtual IExpression Source { get; set; }
        public virtual IBlockList<IWith, With> WithBlocks { get; set; }
        public virtual OptionalReference<IScope> ElseInstructions { get; set; }
    }
}
