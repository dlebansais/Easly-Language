namespace BaseNode;

/// <summary>
/// Represents an indexer type.
/// /Doc/Nodes/Type/IndexerType.md explains the semantic.
/// </summary>
[System.Serializable]
public class IndexerType : ObjectType
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public IndexerType()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        BaseType = default!;
        EntityType = default!;
        IndexParameterBlocks = default!;
        ParameterEnd = default!;
        IndexerKind = default!;
        GetRequireBlocks = default!;
        GetEnsureBlocks = default!;
        GetExceptionIdentifierBlocks = default!;
        SetRequireBlocks = default!;
        SetEnsureBlocks = default!;
        SetExceptionIdentifierBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="IndexerType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="baseType">The base type.</param>
    /// <param name="entityType">The index type.</param>
    /// <param name="indexParameterBlocks">The list of parameters.</param>
    /// <param name="parameterEnd">Whether the command accepts extra parameters.</param>
    /// <param name="indexerKind">How this indexer can be used.</param>
    /// <param name="getRequireBlocks">The getter requirements.</param>
    /// <param name="getEnsureBlocks">The getter guarantees.</param>
    /// <param name="getExceptionIdentifierBlocks">The getter exception handlers.</param>
    /// <param name="setRequireBlocks">The setter requirements.</param>
    /// <param name="setEnsureBlocks">The setter guarantees.</param>
    /// <param name="setExceptionIdentifierBlocks">The setter exception handlers.</param>
    internal IndexerType(Document documentation, ObjectType baseType, ObjectType entityType, IBlockList<EntityDeclaration> indexParameterBlocks, ParameterEndStatus parameterEnd, UtilityType indexerKind, IBlockList<Assertion> getRequireBlocks, IBlockList<Assertion> getEnsureBlocks, IBlockList<Identifier> getExceptionIdentifierBlocks, IBlockList<Assertion> setRequireBlocks, IBlockList<Assertion> setEnsureBlocks, IBlockList<Identifier> setExceptionIdentifierBlocks)
        : base(documentation)
    {
        BaseType = baseType;
        EntityType = entityType;
        IndexParameterBlocks = indexParameterBlocks;
        ParameterEnd = parameterEnd;
        IndexerKind = indexerKind;
        GetRequireBlocks = getRequireBlocks;
        GetEnsureBlocks = getEnsureBlocks;
        GetExceptionIdentifierBlocks = getExceptionIdentifierBlocks;
        SetRequireBlocks = setRequireBlocks;
        SetEnsureBlocks = setEnsureBlocks;
        SetExceptionIdentifierBlocks = setExceptionIdentifierBlocks;
    }

    /// <summary>
    /// Gets or sets the base type.
    /// </summary>
    public virtual ObjectType BaseType { get; set; }

    /// <summary>
    /// Gets or sets the index type.
    /// </summary>
    public virtual ObjectType EntityType { get; set; }

    /// <summary>
    /// Gets or sets the list of parameters.
    /// </summary>
    public virtual IBlockList<EntityDeclaration> IndexParameterBlocks { get; set; }

    /// <summary>
    /// Gets or sets whether the command accepts extra parameters.
    /// </summary>
    public virtual ParameterEndStatus ParameterEnd { get; set; }

    /// <summary>
    /// Gets or sets how this indexer can be used.
    /// </summary>
    public virtual UtilityType IndexerKind { get; set; }

    /// <summary>
    /// Gets or sets the getter requirements.
    /// </summary>
    public virtual IBlockList<Assertion> GetRequireBlocks { get; set; }

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
    /// Gets or sets the setter guarantees.
    /// </summary>
    public virtual IBlockList<Assertion> SetEnsureBlocks { get; set; }

    /// <summary>
    /// Gets or sets the setter exception handlers.
    /// </summary>
    public virtual IBlockList<Identifier> SetExceptionIdentifierBlocks { get; set; }
}
