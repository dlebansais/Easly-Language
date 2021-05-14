#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IIndexerFeature : IFeature
    {
        IObjectType EntityType { get; }
        IBlockList<IEntityDeclaration, EntityDeclaration> IndexParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; }
        IOptionalReference<IBody> GetterBody { get; }
        IOptionalReference<IBody> SetterBody { get; }
    }

    [System.Serializable]
    public class IndexerFeature : Feature, IIndexerFeature
    {
        public virtual IObjectType EntityType { get; set; }
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> IndexParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; set; }
        public virtual IOptionalReference<IBody> GetterBody { get; set; }
        public virtual IOptionalReference<IBody> SetterBody { get; set; }
    }
}
