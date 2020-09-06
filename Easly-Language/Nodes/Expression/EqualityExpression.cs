#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IEqualityExpression : IExpression
    {
        IExpression LeftExpression { get; }
        ComparisonType Comparison { get; }
        EqualityType Equality { get; }
        IExpression RightExpression { get; }
    }

    [System.Serializable]
    public class EqualityExpression : Expression, IEqualityExpression
    {
        public virtual IExpression LeftExpression { get; set; }
        public virtual ComparisonType Comparison { get; set; }
        public virtual EqualityType Equality { get; set; }
        public virtual IExpression RightExpression { get; set; }
    }
}
