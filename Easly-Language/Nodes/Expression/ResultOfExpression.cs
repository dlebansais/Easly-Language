namespace BaseNode
{
    public interface IResultOfExpression : IExpression
    {
        IExpression Source { get; }
    }

    [System.Serializable]
    public class ResultOfExpression : Expression, IResultOfExpression
    {
        public virtual IExpression Source { get; set; }
    }
}
