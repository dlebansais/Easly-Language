#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IInitializedObjectExpression : IExpression
    {
        IIdentifier ClassIdentifier { get; }
        IBlockList<IAssignmentArgument, AssignmentArgument> AssignmentBlocks { get; }
    }

    [System.Serializable]
    public class InitializedObjectExpression : Expression, IInitializedObjectExpression
    {
        public virtual IIdentifier ClassIdentifier { get; set; }
        public virtual IBlockList<IAssignmentArgument, AssignmentArgument> AssignmentBlocks { get; set; }
    }
}
