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
        /// Initializes a new instance of the <see cref="Library"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="entityName">The library name.</param>
        /// <param name="fromIdentifier">The set this library is from.</param>
        /// <param name="importBlocks">The list of imports.</param>
        /// <param name="classIdentifierBlocks">The list of classes in this library.</param>
        internal Library(Document documentation, Name entityName, IOptionalReference<Identifier> fromIdentifier, IBlockList<Import> importBlocks, IBlockList<Identifier> classIdentifierBlocks)
            : base(documentation)
        {
            EntityName = entityName;
            FromIdentifier = fromIdentifier;
            ImportBlocks = importBlocks;
            ClassIdentifierBlocks = classIdentifierBlocks;
        }

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        public virtual Name EntityName { get; set; }

        /// <summary>
        /// Gets or sets the set this library is from.
        /// </summary>
        public virtual IOptionalReference<Identifier> FromIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the list of imports.
        /// </summary>
        public virtual IBlockList<Import> ImportBlocks { get; set; }

        /// <summary>
        /// Gets or sets the list of classes in this library.
        /// </summary>
        public virtual IBlockList<Identifier> ClassIdentifierBlocks { get; set; }
    }
}
