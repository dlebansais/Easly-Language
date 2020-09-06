namespace BaseNode
{
    using Easly;

    public interface IOverLoopInstruction : IInstruction
    {
        IExpression OverList { get; }
        IBlockList<IName, Name> IndexerBlocks { get; }
        IterationType Iteration { get; }
        IScope LoopInstructions { get; }
        IOptionalReference<IIdentifier> ExitEntityName { get; }
        IBlockList<IAssertion, Assertion> InvariantBlocks { get; }
    }

    [System.Serializable]
    public class OverLoopInstruction : Instruction, IOverLoopInstruction
    {
        public virtual IExpression OverList { get; set; }
        public virtual IBlockList<IName, Name> IndexerBlocks { get; set; }
        public virtual IterationType Iteration { get; set; }
        public virtual IScope LoopInstructions { get; set; }
        public virtual IOptionalReference<IIdentifier> ExitEntityName { get; set; }
        public virtual IBlockList<IAssertion, Assertion> InvariantBlocks { get; set; }
    }
}
