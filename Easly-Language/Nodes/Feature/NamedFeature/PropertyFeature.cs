#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

    public interface IPropertyFeature : INamedFeature
    {
        IObjectType EntityType { get; }
        UtilityType PropertyKind { get; }
        IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; }
        IOptionalReference<IBody> GetterBody { get; }
        IOptionalReference<IBody> SetterBody { get; }
    }

    [System.Serializable]
    public class PropertyFeature : NamedFeature, IPropertyFeature
    {
        public virtual IObjectType EntityType { get; set; }
        public virtual UtilityType PropertyKind { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; set; }
        public virtual IOptionalReference<IBody> GetterBody { get; set; }
        public virtual IOptionalReference<IBody> SetterBody { get; set; }
    }
}
