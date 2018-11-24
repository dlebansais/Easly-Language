using Easly;

namespace BaseNode
{
    public interface IGeneric : INode
    {
        IName EntityName { get; }
        OptionalReference<IObjectType> DefaultValue { get; }
        IBlockList<IConstraint, Constraint> ConstraintBlocks { get; }
    }

    [System.Serializable]
    public class Generic : Node, IGeneric
    {
        public virtual IName EntityName { get; set; }
        public virtual OptionalReference<IObjectType> DefaultValue { get; set; }
        public virtual IBlockList<IConstraint, Constraint> ConstraintBlocks { get; set; }
    }
}
