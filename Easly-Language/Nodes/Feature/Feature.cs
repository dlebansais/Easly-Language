namespace BaseNode;

/// <summary>
/// Represents any feature.
/// </summary>
public abstract class Feature : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Feature"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exportIdentifier">The export to which this feature belongs.</param>
    /// <param name="export">The export type.</param>
    internal Feature(Document documentation, Identifier exportIdentifier, ExportStatus export)
        : base(documentation)
    {
        ExportIdentifier = exportIdentifier;
        Export = export;
    }

    /// <summary>
    /// Gets or sets the export to which this feature belongs.
    /// </summary>
    public virtual Identifier ExportIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the export type.
    /// </summary>
    public virtual ExportStatus Export { get; set; }
}
