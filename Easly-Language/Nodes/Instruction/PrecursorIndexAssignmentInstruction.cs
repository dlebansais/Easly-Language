using Easly;

namespace BaseNode
{
    public interface IPrecursorIndexAssignmentInstruction : IInstruction
    {
        OptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class PrecursorIndexAssignmentInstruction : Instruction, IPrecursorIndexAssignmentInstruction
    {
        public virtual OptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
