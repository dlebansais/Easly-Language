#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class ForLoopInstruction : Instruction
    {
        public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; }
        public virtual IBlockList<Instruction> InitInstructionBlocks { get; set; }
        public virtual Expression WhileCondition { get; set; }
        public virtual IBlockList<Instruction> LoopInstructionBlocks { get; set; }
        public virtual IBlockList<Instruction> IterationInstructionBlocks { get; set; }
        public virtual IBlockList<Assertion> InvariantBlocks { get; set; }
        public virtual IOptionalReference<Expression> Variant { get; set; }
    }
}
