#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IQueryOverload : INode
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        IBlockList<IEntityDeclaration, EntityDeclaration> ResultBlocks { get; }
        IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; }
        IOptionalReference<IExpression> Variant { get; }
        IBody QueryBody { get; }
    }

    [System.Serializable]
    public class QueryOverload : Node, IQueryOverload
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> ResultBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; set; }
        public virtual IOptionalReference<IExpression> Variant { get; set; }
        public virtual IBody QueryBody { get; set; }
    }
}
