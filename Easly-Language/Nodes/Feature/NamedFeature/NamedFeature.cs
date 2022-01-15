namespace BaseNode
{
    /// <summary>
    /// Represents any feature with a name.
    /// </summary>
    public abstract class NamedFeature : Feature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedFeature"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="exportIdentifier">The export to which this feature belongs.</param>
        /// <param name="export">The export type.</param>
        /// <param name="entityName">The feature name.</param>
        internal NamedFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName)
            : base(documentation, exportIdentifier, export)
        {
            EntityName = entityName;
        }

        /// <summary>
        /// Gets or sets the feature name.
        /// </summary>
        public virtual Name EntityName { get; set; }
    }
}
