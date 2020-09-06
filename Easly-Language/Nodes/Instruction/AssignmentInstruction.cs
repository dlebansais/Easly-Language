#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IAssignmentInstruction : IInstruction
    {
        IBlockList<IQualifiedName, QualifiedName> DestinationBlocks { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class AssignmentInstruction : Instruction, IAssignmentInstruction
    {
        public virtual IBlockList<IQualifiedName, QualifiedName> DestinationBlocks { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
