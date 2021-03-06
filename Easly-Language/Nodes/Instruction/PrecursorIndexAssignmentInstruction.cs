#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IPrecursorIndexAssignmentInstruction : IInstruction
    {
        IOptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class PrecursorIndexAssignmentInstruction : Instruction, IPrecursorIndexAssignmentInstruction
    {
        public virtual IOptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
