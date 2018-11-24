namespace BaseNode
{
    public interface IQueryExpression : IExpression
    {
        IQualifiedName Query { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class QueryExpression : Expression, IQueryExpression
    {
        public virtual IQualifiedName Query { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
