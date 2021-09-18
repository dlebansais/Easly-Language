namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a library import statement.
    /// /Doc/Nodes/Import.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Import : Node
    {
        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public virtual Identifier LibraryIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set this library is from.
        /// </summary>
        public virtual IOptionalReference<Identifier> FromIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the import type.
        /// </summary>
        public virtual ImportType Type { get; set; }

        /// <summary>
        /// Gets or sets the list of renamed features.
        /// </summary>
        public virtual IBlockList<Rename> RenameBlocks { get; set; } = null!;
    }
}
