#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
