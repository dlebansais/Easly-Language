#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
