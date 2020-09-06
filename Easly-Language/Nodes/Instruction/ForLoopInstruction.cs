namespace BaseNode
{
    using Easly;

    public interface IForLoopInstruction : IInstruction
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; }
        IBlockList<IInstruction, Instruction> InitInstructionBlocks { get; }
        IExpression WhileCondition { get; }
        IBlockList<IInstruction, Instruction> LoopInstructionBlocks { get; }
        IBlockList<IInstruction, Instruction> IterationInstructionBlocks { get; }
        IBlockList<IAssertion, Assertion> InvariantBlocks { get; }
        IOptionalReference<IExpression> Variant { get; }
    }

    [System.Serializable]
    public class ForLoopInstruction : Instruction, IForLoopInstruction
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; set; }
        public virtual IBlockList<IInstruction, Instruction> InitInstructionBlocks { get; set; }
        public virtual IExpression WhileCondition { get; set; }
        public virtual IBlockList<IInstruction, Instruction> LoopInstructionBlocks { get; set; }
        public virtual IBlockList<IInstruction, Instruction> IterationInstructionBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> InvariantBlocks { get; set; }
        public virtual IOptionalReference<IExpression> Variant { get; set; }
    }
}
