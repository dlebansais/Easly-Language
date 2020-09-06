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
