namespace BaseNode
{
    public interface IUnaryNotExpression : IExpression
    {
        IExpression RightExpression { get; }
    }

    [System.Serializable]
    public class UnaryNotExpression : Expression, IUnaryNotExpression
    {
        public virtual IExpression RightExpression { get; set; }
    }
}
