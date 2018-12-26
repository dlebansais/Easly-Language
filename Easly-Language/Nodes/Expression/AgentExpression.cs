using Easly;

namespace BaseNode
{
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
