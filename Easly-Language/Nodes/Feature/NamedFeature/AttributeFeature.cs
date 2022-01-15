namespace BaseNode
{
    /// <summary>
    /// Represents an attribute feature.
    /// /Doc/Nodes/Feature/AttributeFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AttributeFeature : NamedFeature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeFeature"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="exportIdentifier">The export to which this feature belongs.</param>
        /// <param name="export">The export type.</param>
        /// <param name="entityName">The attribute name.</param>
        /// <param name="entityType">The attribute type.</param>
        /// <param name="ensureBlocks">Attribute guarantees.</param>
        internal AttributeFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, IBlockList<Assertion> ensureBlocks)
            : base(documentation, exportIdentifier, export, entityName)
        {
            EntityType = entityType;
            EnsureBlocks = ensureBlocks;
        }

        /// <summary>
        /// Gets or sets the attribute type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; }

        /// <summary>
        /// Gets or sets attribute guarantees.
        /// </summary>
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; }
    }
}
