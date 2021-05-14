#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
