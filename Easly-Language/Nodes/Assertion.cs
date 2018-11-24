using Easly;

namespace BaseNode
{
    public interface IAssertion : INode
    {
        OptionalReference<IName> Tag { get; }
        IExpression BooleanExpression { get; }
    }

    [System.Serializable]
    public class Assertion : Node, IAssertion
    {
        public virtual OptionalReference<IName> Tag { get; set; }
        public virtual IExpression BooleanExpression { get; set; }
    }
}
