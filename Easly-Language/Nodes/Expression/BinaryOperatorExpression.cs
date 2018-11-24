namespace BaseNode
{
    public interface IBinaryOperatorExpression : IExpression
    {
        IExpression LeftExpression { get; }
        IIdentifier Operator { get; }
        IExpression RightExpression { get; }
    }

    [System.Serializable]
    public class BinaryOperatorExpression : Expression, IBinaryOperatorExpression
    {
        public virtual IExpression LeftExpression { get; set; }
        public virtual IIdentifier Operator { get; set; }
        public virtual IExpression RightExpression { get; set; }
    }
}