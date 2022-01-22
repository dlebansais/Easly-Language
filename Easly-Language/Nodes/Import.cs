namespace BaseNode;

using Easly;

/// <summary>
/// Represents a library import statement.
/// /Doc/Nodes/Import.md explains the semantic.
/// </summary>
[System.Serializable]
public class Import : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Import()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        LibraryIdentifier = default!;
        FromIdentifier = default!;
        Type = default!;
        RenameBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Import"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="libraryIdentifier">The library identifier.</param>
    /// <param name="fromIdentifier">The set this library is from.</param>
    /// <param name="type">The import type.</param>
    /// <param name="renameBlocks">The list of renamed features.</param>
    internal Import(Document documentation, Identifier libraryIdentifier, IOptionalReference<Identifier> fromIdentifier, ImportType type, IBlockList<Rename> renameBlocks)
        : base(documentation)
    {
        LibraryIdentifier = libraryIdentifier;
        FromIdentifier = fromIdentifier;
        Type = type;
        RenameBlocks = renameBlocks;
    }

    /// <summary>
    /// Gets or sets the library identifier.
    /// </summary>
    public virtual Identifier LibraryIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the set this library is from.
    /// </summary>
    public virtual IOptionalReference<Identifier> FromIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the import type.
    /// </summary>
    public virtual ImportType Type { get; set; }

    /// <summary>
    /// Gets or sets the list of renamed features.
    /// </summary>
    public virtual IBlockList<Rename> RenameBlocks { get; set; }
}
