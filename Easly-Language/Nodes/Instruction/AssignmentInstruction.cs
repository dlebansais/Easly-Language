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
