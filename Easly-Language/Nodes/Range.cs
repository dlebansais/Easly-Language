#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

    public interface IRange : INode
    {
        IExpression LeftExpression { get; }
        IOptionalReference<IExpression> RightExpression { get; }
    }

    [System.Serializable]
    public class Range : Node, IRange
    {
        public virtual IExpression LeftExpression { get; set; }
        public virtual IOptionalReference<IExpression> RightExpression { get; set; }
    }
}
