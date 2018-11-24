namespace BaseNode
{
    public interface INamedFeature : IFeature
    {
        IName EntityName { get; }
    }

    public abstract class NamedFeature : Feature, INamedFeature
    {
        public virtual IName EntityName { get; set; }
    }
}
