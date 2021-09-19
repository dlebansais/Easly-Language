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
        /// Gets or sets the property type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets how this property can be used.
        /// </summary>
        public virtual UtilityType PropertyKind { get; set; } = UtilityType.ReadOnly;

        /// <summary>
        /// Gets or sets the list of other features this property modifies.
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
