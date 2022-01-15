namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a property feature.
    /// /Doc/Nodes/Feature/PropertyFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PropertyFeature : NamedFeature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyFeature"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="exportIdentifier">The export to which this feature belongs.</param>
        /// <param name="export">The export type.</param>
        /// <param name="entityName">The property name.</param>
        /// <param name="entityType">The property type.</param>
        /// <param name="propertyKind">How this property can be used.</param>
        /// <param name="modifiedQueryBlocks">The list of other features this property modifies.</param>
        /// <param name="getterBody">The getter body.</param>
        /// <param name="setterBody">The setter body.</param>
        internal PropertyFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, UtilityType propertyKind, IBlockList<Identifier> modifiedQueryBlocks, IOptionalReference<Body> getterBody, IOptionalReference<Body> setterBody)
            : base(documentation, exportIdentifier, export, entityName)
        {
            EntityType = entityType;
            PropertyKind = propertyKind;
            ModifiedQueryBlocks = modifiedQueryBlocks;
            GetterBody = getterBody;
            SetterBody = setterBody;
        }

        /// <summary>
        /// Gets or sets the property type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; }

        /// <summary>
        /// Gets or sets how this property can be used.
        /// </summary>
        public virtual UtilityType PropertyKind { get; set; }

        /// <summary>
        /// Gets or sets the list of other features this property modifies.
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
