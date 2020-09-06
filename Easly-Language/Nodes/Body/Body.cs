#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IBody : INode
    {
        IBlockList<IAssertion, Assertion> RequireBlocks { get; }
        IBlockList<IAssertion, Assertion> EnsureBlocks { get; }
        IBlockList<IIdentifier, Identifier> ExceptionIdentifierBlocks { get; }
    }

    public abstract class Body : Node, IBody
    {
        public virtual IBlockList<IAssertion, Assertion> RequireBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> EnsureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ExceptionIdentifierBlocks { get; set; }
    }
}
