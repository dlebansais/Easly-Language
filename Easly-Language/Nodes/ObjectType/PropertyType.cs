namespace BaseNode;

/// <summary>
/// Represents a property type.
/// /Doc/Nodes/Type/PropertyType.md explains the semantic.
/// </summary>
[System.Serializable]
public class PropertyType : ObjectType
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public PropertyType()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        BaseType = default!;
        EntityType = default!;
        PropertyKind = default!;
        GetEnsureBlocks = default!;
        GetExceptionIdentifierBlocks = default!;
        SetRequireBlocks = default!;
        SetExceptionIdentifierBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The property type.</param>
    /// <param name="propertyKind">How this property can be used.</param>
    /// <param name="getEnsureBlocks">The getter guarantees.</param>
    /// <param name="getExceptionIdentifierBlocks">The getter exception handlers.</param>
    /// <param name="setRequireBlocks">The setter requirements.</param>
    /// <param name="setExceptionIdentifierBlocks">The setter exception handlers.</param>
    internal PropertyType(Document documentation, ObjectType baseType, ObjectType entityType, UtilityType propertyKind, IBlockList<Assertion> getEnsureBlocks, IBlockList<Identifier> getExceptionIdentifierBlocks, IBlockList<Assertion> setRequireBlocks, IBlockList<Identifier> setExceptionIdentifierBlocks)
        : base(documentation)
    {
        BaseType = baseType;
        EntityType = entityType;
        PropertyKind = propertyKind;
        GetEnsureBlocks = getEnsureBlocks;
        GetExceptionIdentifierBlocks = getExceptionIdentifierBlocks;
        SetRequireBlocks = setRequireBlocks;
        SetExceptionIdentifierBlocks = setExceptionIdentifierBlocks;
    }

    /// <summary>
    /// Gets or sets the base type.
    /// </summary>
    public virtual ObjectType BaseType { get; set; }

    /// <summary>
    /// Gets or sets the property type.
    /// </summary>
    public virtual ObjectType EntityType { get; set; }

    /// <summary>
    /// Gets or sets how this property can be used.
    /// </summary>
    public virtual UtilityType PropertyKind { get; set; }

    /// <summary>
    /// Gets or sets the getter guarantees.
    /// </summary>
    public virtual IBlockList<Assertion> GetEnsureBlocks { get; set; }

    /// <summary>
    /// Gets or sets the getter exception handlers.
    /// </summary>
    public virtual IBlockList<Identifier> GetExceptionIdentifierBlocks { get; set; }

    /// <summary>
    /// Gets or sets the setter requirements.
    /// </summary>
    public virtual IBlockList<Assertion> SetRequireBlocks { get; set; }

    /// <summary>
    /// Gets or sets the setter exception handlers.
    /// </summary>
    public virtual IBlockList<Identifier> SetExceptionIdentifierBlocks { get; set; }
}
