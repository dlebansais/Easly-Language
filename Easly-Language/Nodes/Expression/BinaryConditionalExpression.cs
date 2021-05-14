#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public interface IBinaryConditionalExpression : IExpression
    {
        IExpression LeftExpression { get; }
        ConditionalTypes Conditional { get; }
        IExpression RightExpression { get; }
    }

    [System.Serializable]
    public class BinaryConditionalExpression : Expression, IBinaryConditionalExpression
    {
        public virtual IExpression LeftExpression { get; set; }
        public virtual ConditionalTypes Conditional { get; set; }
        public virtual IExpression RightExpression { get; set; }
    }
}
