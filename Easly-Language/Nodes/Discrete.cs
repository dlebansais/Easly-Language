#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
