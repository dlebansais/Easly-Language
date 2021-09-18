#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class QueryOverload : Node
    {
        public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBlockList<EntityDeclaration> ResultBlocks { get; set; }
        public virtual IBlockList<Identifier> ModifiedQueryBlocks { get; set; }
        public virtual IOptionalReference<Expression> Variant { get; set; }
        public virtual Body QueryBody { get; set; }
    }
}
