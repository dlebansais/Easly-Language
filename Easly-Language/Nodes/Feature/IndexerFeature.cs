using Easly;

namespace BaseNode
{
    public interface IIndexerFeature : IFeature
    {
        IObjectType EntityType { get; }
        IBlockList<IEntityDeclaration, EntityDeclaration> IndexParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; }
        OptionalReference<IBody> GetterBody { get; }
        OptionalReference<IBody> SetterBody { get; }
    }

    [System.Serializable]
    public class IndexerFeature : Feature, IIndexerFeature
    {
        public virtual IObjectType EntityType { get; set; }
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> IndexParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ModifiedQueryBlocks { get; set; }
        public virtual OptionalReference<IBody> GetterBody { get; set; }
        public virtual OptionalReference<IBody> SetterBody { get; set; }
    }
}
