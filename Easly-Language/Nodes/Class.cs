namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a class.
    /// /Doc/Nodes/Class.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Class : Node
    {
        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set this class is from.
        /// </summary>
        public virtual IOptionalReference<Identifier> FromIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class copy semantic.
        /// </summary>
        public virtual CopySemantic CopySpecification { get; set; } = CopySemantic.Any;

        /// <summary>
        /// Gets or sets whether the class is cloneable.
        /// </summary>
        public virtual CloneableStatus Cloneable { get; set; } = CloneableStatus.Cloneable;

        /// <summary>
        /// Gets or sets whether the class is comparable.
        /// </summary>
        public virtual ComparableStatus Comparable { get; set; } = ComparableStatus.Comparable;

        /// <summary>
        /// Gets or sets a value indicating whether the class is abstract.
        /// </summary>
        public virtual bool IsAbstract { get; set; } = false;

        /// <summary>
        /// Gets or sets the class imports.
        /// </summary>
        public virtual IBlockList<Import> ImportBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class generics.
        /// </summary>
        public virtual IBlockList<Generic> GenericBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class exports.
        /// </summary>
        public virtual IBlockList<Export> ExportBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class typedefs.
        /// </summary>
        public virtual IBlockList<Typedef> TypedefBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class inheritances.
        /// </summary>
        public virtual IBlockList<Inheritance> InheritanceBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class discrete values.
        /// </summary>
        public virtual IBlockList<Discrete> DiscreteBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class replicates.
        /// </summary>
        public virtual IBlockList<ClassReplicate> ClassReplicateBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class features.
        /// </summary>
        public virtual IBlockList<Feature> FeatureBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class conversions.
        /// </summary>
        public virtual IBlockList<Identifier> ConversionBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class invariants.
        /// </summary>
        public virtual IBlockList<Assertion> InvariantBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the class unique ID.
        /// </summary>
        public virtual System.Guid ClassGuid { get; set; } = System.Guid.Empty;

        /// <summary>
        /// Gets or sets the class path.
        /// </summary>
        public virtual string ClassPath { get; set; } = string.Empty;
    }
}
