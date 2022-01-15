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
        /// Initializes a new instance of the <see cref="IndexerFeature"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="exportIdentifier">The export to which this feature belongs.</param>
        /// <param name="export">The export type.</param>
        /// <param name="entityType">The indexed value type.</param>
        /// <param name="indexParameterBlocks">The parameters.</param>
        /// <param name="parameterEnd">Whether the index accepts extra parameters.</param>
        /// <param name="modifiedQueryBlocks">The list of other features this indexer modifies.</param>
        /// <param name="getterBody">The getter body.</param>
        /// <param name="setterBody">The setter body.</param>
        internal IndexerFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, ObjectType entityType, IBlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd, IBlockList<Identifier> modifiedQueryBlocks, IOptionalReference<Body> getterBody, IOptionalReference<Body> setterBody)
            : base(documentation, exportIdentifier, export)
        {
            EntityType = entityType;
            IndexParameterBlocks = indexParameterBlocks;
            ParameterEnd = parameterEnd;
            ModifiedQueryBlocks = modifiedQueryBlocks;
            GetterBody = getterBody;
            SetterBody = setterBody;
        }

        /// <summary>
        /// Gets or sets the indexed value type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> IndexParameterBlocks { get; set; }

        /// <summary>
        /// Gets or sets whether the index accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; }

        /// <summary>
        /// Gets or sets the list of other features this indexer modifies.
        /// </summary>
        public virtual IBlockList<Identifier> ModifiedQueryBlocks { get; set; }

        /// <summary>
        /// Gets or sets the getter body.
        /// </summary>
        public virtual IOptionalReference<Body> GetterBody { get; set; }

        /// <summary>
        /// Gets or sets the setter body.
        /// </summary>
        public virtual IOptionalReference<Body> SetterBody { get; set; }
    }
}
