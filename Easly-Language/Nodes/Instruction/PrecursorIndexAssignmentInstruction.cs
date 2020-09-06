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
