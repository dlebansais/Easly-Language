using Easly;

namespace BaseNode
{
    public interface IPropertyFeature : INamedFeature
    {
        IObjectType EntityType { get; }
        UtilityType PropertyKind { get; }
        IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; }
        OptionalReference<IBody> GetterBody { get; }
        OptionalReference<IBody> SetterBody { get; }
    }

    [System.Serializable]
    public class PropertyFeature : NamedFeature, IPropertyFeature
    {
        public virtual IObjectType EntityType { get; set; }
        public virtual UtilityType PropertyKind { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; set; }
        public virtual OptionalReference<IBody> GetterBody { get; set; }
        public virtual OptionalReference<IBody> SetterBody { get; set; }
    }
}
