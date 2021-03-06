#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public interface IQueryOverloadType : INode
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        IBlockList<IEntityDeclaration, EntityDeclaration> ResultBlocks { get; }
        IBlockList<IAssertion, Assertion> RequireBlocks { get; }
        IBlockList<IAssertion, Assertion> EnsureBlocks { get; }
        IBlockList<IIdentifier, Identifier> ExceptionIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class QueryOverloadType : Node, IQueryOverloadType
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> ResultBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> RequireBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> EnsureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ExceptionIdentifierBlocks { get; set; }
    }
}
