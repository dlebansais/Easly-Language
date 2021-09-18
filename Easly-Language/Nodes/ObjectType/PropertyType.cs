namespace BaseNode
{
    /// <summary>
    /// Represents a property type.
    /// /Doc/Nodes/Type/PropertyType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PropertyType : ObjectType
    {
        /// <summary>
        /// Gets or sets the base type.
        /// </summary>
        public virtual ObjectType BaseType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the property type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets how this property can be used.
        /// </summary>
        public virtual UtilityType PropertyKind { get; set; }

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
        /// Gets or sets setter exception handlers.
        /// </summary>
        public virtual IBlockList<Identifier> SetExceptionIdentifierBlocks { get; set; } = null!;
    }
}
