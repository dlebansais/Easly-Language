namespace BaseNode
{
    using Easly;

    public interface IDiscrete : INode
    {
        IName EntityName { get; }
        IOptionalReference<IExpression> NumericValue { get; }
    }

    [System.Serializable]
    public class Discrete : Node, IDiscrete
    {
        public virtual IName EntityName { get; set; }
        public virtual IOptionalReference<IExpression> NumericValue { get; set; }
    }
}
