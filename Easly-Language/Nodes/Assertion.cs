#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IAssertion : INode
    {
        IOptionalReference<IName> Tag { get; }
        IExpression BooleanExpression { get; }
    }

    [System.Serializable]
    public class Assertion : Node, IAssertion
    {
        public virtual IOptionalReference<IName> Tag { get; set; }
        public virtual IExpression BooleanExpression { get; set; }
    }
}
