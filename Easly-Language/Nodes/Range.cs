using Easly;

namespace BaseNode
{
    public interface IRange : INode
    {
        IExpression LeftExpression { get; }
        OptionalReference<IExpression> RightExpression { get; }
    }

    [System.Serializable]
    public class Range : Node, IRange
    {
        public virtual IExpression LeftExpression { get; set; }
        public virtual OptionalReference<IExpression> RightExpression { get; set; }
    }
}
