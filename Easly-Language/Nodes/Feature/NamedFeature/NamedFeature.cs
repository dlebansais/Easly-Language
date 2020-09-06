#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
