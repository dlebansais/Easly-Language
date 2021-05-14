#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
