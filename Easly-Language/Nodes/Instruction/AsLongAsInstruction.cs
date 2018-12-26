using Easly;

namespace BaseNode
{
    public interface IAsLongAsInstruction : IInstruction
    {
        IExpression ContinueCondition { get; }
        IBlockList<IContinuation, Continuation> ContinuationBlocks { get; }
        IOptionalReference<IScope> ElseInstructions { get; }
    }

    [System.Serializable]
    public class AsLongAsInstruction : Instruction, IAsLongAsInstruction
    {
        public virtual IExpression ContinueCondition { get; set; }
        public virtual IBlockList<IContinuation, Continuation> ContinuationBlocks { get; set; }
        public virtual IOptionalReference<IScope> ElseInstructions { get; set; }
    }
}
