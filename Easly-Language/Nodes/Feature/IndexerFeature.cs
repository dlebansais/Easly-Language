namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an indexer feature.
    /// /Doc/Nodes/Feature/IndexerFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class IndexerFeature : Feature
    {
        /// <summary>
        /// Gets or sets the indexed value type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> IndexParameterBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the index accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; } = ParameterEndStatus.Closed;

        /// <summary>
        /// Gets or sets the list of other features this indexer modifies.
        /// </summary>
        public virtual IBlockList<Identifier> ModifiedQueryBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the getter body.
        /// </summary>
        public virtual IOptionalReference<Body> GetterBody { get; set; } = null!;

        /// <summary>
        /// Gets or sets the setter body.
        /// </summary>
        public virtual IOptionalReference<Body> SetterBody { get; set; } = null!;
    }
}
