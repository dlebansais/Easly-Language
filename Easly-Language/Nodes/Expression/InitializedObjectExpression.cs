#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
