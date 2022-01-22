namespace BaseNode;

/// <summary>
/// Represents a change in the export status of an inherited feature.
/// /Doc/Nodes/ExportChange.md explains the semantic.
/// </summary>
[System.Serializable]
public class ExportChange : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ExportChange()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ExportIdentifier = default!;
        IdentifierBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ExportChange"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exportIdentifier">The modified export.</param>
    /// <param name="identifierBlocks">The new class names.</param>
    internal ExportChange(Document documentation, Identifier exportIdentifier, IBlockList<Identifier> identifierBlocks)
        : base(documentation)
    {
        ExportIdentifier = exportIdentifier;
        IdentifierBlocks = identifierBlocks;
    }

    /// <summary>
    /// Gets or sets the modified export.
    /// </summary>
    public virtual Identifier ExportIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the new class names.
    /// </summary>
    public virtual IBlockList<Identifier> IdentifierBlocks { get; set; }
}
