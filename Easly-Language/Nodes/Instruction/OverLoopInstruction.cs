#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class OverLoopInstruction : Instruction
    {
        public virtual Expression OverList { get; set; }
        public virtual IBlockList<Name> IndexerBlocks { get; set; }
        public virtual IterationType Iteration { get; set; }
        public virtual Scope LoopInstructions { get; set; }
        public virtual IOptionalReference<Identifier> ExitEntityName { get; set; }
        public virtual IBlockList<Assertion> InvariantBlocks { get; set; }
    }
}
