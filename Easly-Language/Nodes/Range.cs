#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
