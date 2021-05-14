#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IAgentExpression : IExpression
    {
        IIdentifier Delegated { get; }
        IOptionalReference<IObjectType> BaseType { get; }
    }

    [System.Serializable]
    public class AgentExpression : Expression, IAgentExpression
    {
        public virtual IIdentifier Delegated { get; set; }
        public virtual IOptionalReference<IObjectType> BaseType { get; set; }
    }
}
