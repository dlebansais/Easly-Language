#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class AsLongAsInstruction : Instruction
    {
        public virtual Expression ContinueCondition { get; set; }
        public virtual BlockList<Continuation> ContinuationBlocks { get; set; }
        public virtual OptionalReference<Scope> ElseInstructions { get; set; }
    }
}
