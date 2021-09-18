namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a library.
    /// /Doc/Nodes/Library.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Library : Node
    {
        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set this library is from.
        /// </summary>
        public virtual IOptionalReference<Identifier> FromIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of imports.
        /// </summary>
        public virtual IBlockList<Import> ImportBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of classes in this library.
        /// </summary>
        public virtual IBlockList<Identifier> ClassIdentifierBlocks { get; set; } = null!;
    }
}
