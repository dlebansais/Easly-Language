namespace BaseNode
{
    /// <summary>
    /// Represents a class inheritance specification.
    /// /Doc/Nodes/Inheritance.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Inheritance : Node
    {
        /// <summary>
        /// Gets or sets the parent type.
        /// </summary>
        public virtual ObjectType ParentType { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the inheritance is for conformance.
        /// </summary>
        public virtual ConformanceType Conformance { get; set; }

        /// <summary>
        /// Gets or sets the list of renames.
        /// </summary>
        public virtual IBlockList<Rename> RenameBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the indexer should be overriden.
        /// </summary>
        public virtual bool ForgetIndexer { get; set; }

        /// <summary>
        /// Gets or sets the list of overriden features.
        /// </summary>
        public virtual IBlockList<Identifier> ForgetBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the indexer should be kept.
        /// </summary>
        public virtual bool KeepIndexer { get; set; }

        /// <summary>
        /// Gets or sets the list of kept features.
        /// </summary>
        public virtual IBlockList<Identifier> KeepBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the indexer should be discontinued.
        /// </summary>
        public virtual bool DiscontinueIndexer { get; set; }

        /// <summary>
        /// Gets or sets the list of discontinued features.
        /// </summary>
        public virtual IBlockList<Identifier> DiscontinueBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of changed export statements.
        /// </summary>
        public virtual IBlockList<ExportChange> ExportChangeBlocks { get; set; } = null!;
    }
}
