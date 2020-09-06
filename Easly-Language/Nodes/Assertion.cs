#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
