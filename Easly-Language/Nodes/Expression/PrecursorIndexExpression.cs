using Easly;

namespace BaseNode
{
    public interface IPrecursorIndexExpression : IExpression
    {
        OptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class PrecursorIndexExpression : Expression, IPrecursorIndexExpression
    {
        public virtual OptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
