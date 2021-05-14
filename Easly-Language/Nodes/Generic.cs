#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IGeneric : INode
    {
        IName EntityName { get; }
        IOptionalReference<IObjectType> DefaultValue { get; }
        IBlockList<IConstraint, Constraint> ConstraintBlocks { get; }
    }

    [System.Serializable]
    public class Generic : Node, IGeneric
    {
        public virtual IName EntityName { get; set; }
        public virtual IOptionalReference<IObjectType> DefaultValue { get; set; }
        public virtual IBlockList<IConstraint, Constraint> ConstraintBlocks { get; set; }
    }
}
