#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public abstract class Body : Node
    {
        public virtual IBlockList<Assertion> RequireBlocks { get; set; }
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; }
        public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; }
    }
}
