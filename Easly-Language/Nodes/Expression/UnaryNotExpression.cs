#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
