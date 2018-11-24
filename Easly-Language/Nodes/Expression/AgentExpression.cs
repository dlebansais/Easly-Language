using Easly;

namespace BaseNode
{
    public interface IAgentExpression : IExpression
    {
        IIdentifier Delegated { get; }
        OptionalReference<IObjectType> BaseType { get; }
    }

    [System.Serializable]
    public class AgentExpression : Expression, IAgentExpression
    {
        public virtual IIdentifier Delegated { get; set; }
        public virtual OptionalReference<IObjectType> BaseType { get; set; }
    }
}
