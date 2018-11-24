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
