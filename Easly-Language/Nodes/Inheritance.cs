namespace BaseNode;

/// <summary>
/// Represents a class inheritance specification.
/// /Doc/Nodes/Inheritance.md explains the semantic.
/// </summary>
[System.Serializable]
public class Inheritance : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Inheritance()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ParentType = default!;
        Conformance = default!;
        RenameBlocks = default!;
        ForgetIndexer = default!;
        ForgetBlocks = default!;
        KeepIndexer = default!;
        KeepBlocks = default!;
        DiscontinueIndexer = default!;
        DiscontinueBlocks = default!;
        ExportChangeBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Inheritance"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parentType">The parent type.</param>
    /// <param name="conformance">Whether the inheritance is for conformance.</param>
    /// <param name="renameBlocks">The list of renames.</param>
    /// <param name="forgetIndexer">Whether the indexer should be overriden.</param>
    /// <param name="forgetBlocks">The list of overriden features.</param>
    /// <param name="keepIndexer">Whether the indexer should be kept.</param>
    /// <param name="keepBlocks">The list of kept features.</param>
    /// <param name="discontinueIndexer">Whether the indexer should be discontinued.</param>
    /// <param name="discontinueBlocks">The list of discontinued features.</param>
    /// <param name="exportChangeBlocks">The list of changed export statements.</param>
    internal Inheritance(Document documentation, ObjectType parentType, ConformanceType conformance, IBlockList<Rename> renameBlocks, bool forgetIndexer, IBlockList<Identifier> forgetBlocks, bool keepIndexer, IBlockList<Identifier> keepBlocks, bool discontinueIndexer, IBlockList<Identifier> discontinueBlocks, IBlockList<ExportChange> exportChangeBlocks)
        : base(documentation)
    {
        ParentType = parentType;
        Conformance = conformance;
        RenameBlocks = renameBlocks;
        ForgetIndexer = forgetIndexer;
        ForgetBlocks = forgetBlocks;
        KeepIndexer = keepIndexer;
        KeepBlocks = keepBlocks;
        DiscontinueIndexer = discontinueIndexer;
        DiscontinueBlocks = discontinueBlocks;
        ExportChangeBlocks = exportChangeBlocks;
    }

    /// <summary>
    /// Gets or sets the parent type.
    /// </summary>
    public virtual ObjectType ParentType { get; set; }

    /// <summary>
    /// Gets or sets whether the inheritance is for conformance.
    /// </summary>
    public virtual ConformanceType Conformance { get; set; }

    /// <summary>
    /// Gets or sets the list of renames.
    /// </summary>
    public virtual IBlockList<Rename> RenameBlocks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the indexer should be overriden.
    /// </summary>
    public virtual bool ForgetIndexer { get; set; }

    /// <summary>
    /// Gets or sets the list of overriden features.
    /// </summary>
    public virtual IBlockList<Identifier> ForgetBlocks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the indexer should be kept.
    /// </summary>
    public virtual bool KeepIndexer { get; set; }

    /// <summary>
    /// Gets or sets the list of kept features.
    /// </summary>
    public virtual IBlockList<Identifier> KeepBlocks { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the indexer should be discontinued.
    /// </summary>
    public virtual bool DiscontinueIndexer { get; set; }

    /// <summary>
    /// Gets or sets the list of discontinued features.
    /// </summary>
    public virtual IBlockList<Identifier> DiscontinueBlocks { get; set; }

    /// <summary>
    /// Gets or sets the list of changed export statements.
    /// </summary>
    public virtual IBlockList<ExportChange> ExportChangeBlocks { get; set; }
}
