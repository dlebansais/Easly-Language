#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IConstantFeature : INamedFeature
    {
        IObjectType EntityType { get; }
        IExpression ConstantValue { get; }
    }

    [System.Serializable]
    public class ConstantFeature : NamedFeature, IConstantFeature
    {
        public virtual IObjectType EntityType { get; set; }
        public virtual IExpression ConstantValue { get; set; }
    }
}
