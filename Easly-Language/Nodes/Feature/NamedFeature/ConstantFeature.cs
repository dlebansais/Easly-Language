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
