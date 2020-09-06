#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

    public interface IPrecursorExpression : IExpression
    {
        IOptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class PrecursorExpression : Expression, IPrecursorExpression
    {
        public virtual IOptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
