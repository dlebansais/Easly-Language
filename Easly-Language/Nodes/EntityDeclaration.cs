using Easly;

namespace BaseNode
{
    public interface IEntityDeclaration : INode
    {
        IName EntityName { get; }
        IObjectType EntityType { get; }
        OptionalReference<IExpression> DefaultValue { get; }
    }

    [System.Serializable]
    public class EntityDeclaration : Node, IEntityDeclaration
    {
        public virtual IName EntityName { get; set; }
        public virtual IObjectType EntityType { get; set; }
        public virtual OptionalReference<IExpression> DefaultValue { get; set; }
    }
}
