#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IIndexAssignmentInstruction : IInstruction
    {
        IQualifiedName Destination { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class IndexAssignmentInstruction : Instruction, IIndexAssignmentInstruction
    {
        public virtual IQualifiedName Destination { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
