using Easly;

namespace BaseNode
{
    public interface IPrecursorExpression : IExpression
    {
        OptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class PrecursorExpression : Expression, IPrecursorExpression
    {
        public virtual OptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
