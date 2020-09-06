#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IIndexQueryExpression : IExpression
    {
        IExpression IndexedExpression { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class IndexQueryExpression : Expression, IIndexQueryExpression
    {
        public virtual IExpression IndexedExpression { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
