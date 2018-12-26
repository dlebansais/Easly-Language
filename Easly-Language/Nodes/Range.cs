using Easly;

namespace BaseNode
{
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
