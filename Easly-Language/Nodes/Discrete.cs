using Easly;

namespace BaseNode
{
    public interface IDiscrete : INode
    {
        IName EntityName { get; }
        OptionalReference<IExpression> NumericValue { get; }
    }

    [System.Serializable]
    public class Discrete : Node, IDiscrete
    {
        public virtual IName EntityName { get; set; }
        public virtual OptionalReference<IExpression> NumericValue { get; set; }
    }
}
