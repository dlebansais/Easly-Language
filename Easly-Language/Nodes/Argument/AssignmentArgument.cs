#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IAssignmentArgument : IArgument
    {
        IBlockList<IIdentifier, Identifier> ParameterBlocks { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class AssignmentArgument : Argument, IAssignmentArgument
    {
        public virtual IBlockList<IIdentifier, Identifier> ParameterBlocks { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
