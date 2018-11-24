namespace BaseNode
{
    public interface IUnaryOperatorExpression : IExpression
    {
        IIdentifier Operator { get; }
        IExpression RightExpression { get; }
    }

    [System.Serializable]
    public class UnaryOperatorExpression : Expression, IUnaryOperatorExpression
    {
        public virtual IIdentifier Operator { get; set; }
        public virtual IExpression RightExpression { get; set; }
    }
}
