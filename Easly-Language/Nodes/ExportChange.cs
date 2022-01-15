namespace BaseNode;

/// <summary>
/// Represents a change in the export status of an inherited feature.
/// /Doc/Nodes/ExportChange.md explains the semantic.
/// </summary>
[System.Serializable]
public class ExportChange : Node
{
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
