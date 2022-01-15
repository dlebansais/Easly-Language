namespace BaseNode;

using Easly;

/// <summary>
/// Represents a class.
/// /Doc/Nodes/Class.md explains the semantic.
/// </summary>
[System.Serializable]
public class Class : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Class"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityName">The class name.</param>
    /// <param name="fromIdentifier">The set this class is from.</param>
    /// <param name="copySpecification">The class copy semantic.</param>
    /// <param name="cloneable">Whether the class is cloneable.</param>
    /// <param name="comparable">Whether the class is comparable.</param>
    /// <param name="isAbstract">Whether the class is abstract.</param>
    /// <param name="importBlocks">The class imports.</param>
    /// <param name="genericBlocks">The class generics.</param>
    /// <param name="exportBlocks">The class exports.</param>
    /// <param name="typedefBlocks">The class typedefs.</param>
    /// <param name="inheritanceBlocks">The class inheritances.</param>
    /// <param name="discreteBlocks">The class discrete values.</param>
    /// <param name="classReplicateBlocks">The class replicates.</param>
    /// <param name="featureBlocks">The class features.</param>
    /// <param name="conversionBlocks">The class conversions.</param>
    /// <param name="invariantBlocks">The class invariants.</param>
    /// <param name="classGuid">The class unique ID.</param>
    /// <param name="classPath">The class path.</param>
    internal Class(Document documentation, Name entityName, IOptionalReference<Identifier> fromIdentifier, CopySemantic copySpecification, CloneableStatus cloneable, ComparableStatus comparable, bool isAbstract, IBlockList<Import> importBlocks, IBlockList<Generic> genericBlocks, IBlockList<Export> exportBlocks, IBlockList<Typedef> typedefBlocks, IBlockList<Inheritance> inheritanceBlocks, IBlockList<Discrete> discreteBlocks, IBlockList<ClassReplicate> classReplicateBlocks, IBlockList<Feature> featureBlocks, IBlockList<Identifier> conversionBlocks, IBlockList<Assertion> invariantBlocks, System.Guid classGuid, string classPath)
        : base(documentation)
    {
        EntityName = entityName;
        FromIdentifier = fromIdentifier;
        CopySpecification = copySpecification;
        Cloneable = cloneable;
        Comparable = comparable;
        IsAbstract = isAbstract;
        ImportBlocks = importBlocks;
        GenericBlocks = genericBlocks;
        ExportBlocks = exportBlocks;
        TypedefBlocks = typedefBlocks;
        InheritanceBlocks = inheritanceBlocks;
        DiscreteBlocks = discreteBlocks;
        ClassReplicateBlocks = classReplicateBlocks;
        FeatureBlocks = featureBlocks;
        ConversionBlocks = conversionBlocks;
        InvariantBlocks = invariantBlocks;
        ClassGuid = classGuid;
        ClassPath = classPath;
    }

    /// <summary>
    /// Gets or sets the class name.
    /// </summary>
    public virtual Name EntityName { get; set; }

    /// <summary>
    /// Gets or sets the set this class is from.
    /// </summary>
    public virtual IOptionalReference<Identifier> FromIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the class copy semantic.
    /// </summary>
    public virtual CopySemantic CopySpecification { get; set; }

    /// <summary>
    /// Gets or sets whether the class is cloneable.
    /// </summary>
    public virtual CloneableStatus Cloneable { get; set; }

    /// <summary>
    /// Gets or sets whether the class is comparable.
    /// </summary>
    public virtual ComparableStatus Comparable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the class is abstract.
    /// </summary>
    public virtual bool IsAbstract { get; set; }

    /// <summary>
    /// Gets or sets the class imports.
    /// </summary>
    public virtual IBlockList<Import> ImportBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class generics.
    /// </summary>
    public virtual IBlockList<Generic> GenericBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class exports.
    /// </summary>
    public virtual IBlockList<Export> ExportBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class typedefs.
    /// </summary>
    public virtual IBlockList<Typedef> TypedefBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class inheritances.
    /// </summary>
    public virtual IBlockList<Inheritance> InheritanceBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class discrete values.
    /// </summary>
    public virtual IBlockList<Discrete> DiscreteBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class replicates.
    /// </summary>
    public virtual IBlockList<ClassReplicate> ClassReplicateBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class features.
    /// </summary>
    public virtual IBlockList<Feature> FeatureBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class conversions.
    /// </summary>
    public virtual IBlockList<Identifier> ConversionBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class invariants.
    /// </summary>
    public virtual IBlockList<Assertion> InvariantBlocks { get; set; }

    /// <summary>
    /// Gets or sets the class unique ID.
    /// </summary>
    public virtual System.Guid ClassGuid { get; set; }

    /// <summary>
    /// Gets or sets the class path.
    /// </summary>
    public virtual string ClassPath { get; set; }
}
