namespace BaseNode
{
    /// <summary>
    /// Represents an indexer type.
    /// /Doc/Nodes/Type/IndexerType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class IndexerType : ObjectType
    {
        /// <summary>
        /// Gets or sets the base type.
        /// </summary>
        public virtual ObjectType BaseType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the index type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> IndexParameterBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the command accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; } = ParameterEndStatus.Closed;

        /// <summary>
        /// Gets or sets how this indexer can be used.
        /// </summary>
        public virtual UtilityType IndexerKind { get; set; } = UtilityType.ReadOnly;

        /// <summary>
        /// Gets or sets getter requirements.
        /// </summary>
        public virtual IBlockList<Assertion> GetRequireBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets getter guaranties.
        /// </summary>
        public virtual IBlockList<Assertion> GetEnsureBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets getter exception handlers.
        /// </summary>
        public virtual IBlockList<Identifier> GetExceptionIdentifierBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets setter requirements.
        /// </summary>
        public virtual IBlockList<Assertion> SetRequireBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets setter guaranties.
        /// </summary>
        public virtual IBlockList<Assertion> SetEnsureBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets setter exception handlers.
        /// </summary>
        public virtual IBlockList<Identifier> SetExceptionIdentifierBlocks { get; set; } = null!;
    }
}
