namespace BaseNode;

/// <summary>
/// Represents a function feature.
/// /Doc/Nodes/Feature/FunctionFeature.md explains the semantic.
/// </summary>
[System.Serializable]
public class FunctionFeature : NamedFeature
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FunctionFeature"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exportIdentifier">The export to which this feature belongs.</param>
    /// <param name="export">The export type.</param>
    /// <param name="entityName">The function name.</param>
    /// <param name="once">Whether this function is executed only once.</param>
    /// <param name="overloadBlocks">The list of overloads.</param>
    internal FunctionFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, OnceChoice once, IBlockList<QueryOverload> overloadBlocks)
        : base(documentation, exportIdentifier, export, entityName)
    {
        Once = once;
        OverloadBlocks = overloadBlocks;
    }

    /// <summary>
    /// Gets or sets whether this function is executed only once.
    /// </summary>
    public virtual OnceChoice Once { get; set; }

    /// <summary>
    /// Gets or sets the list of overloads.
    /// </summary>
    public virtual IBlockList<QueryOverload> OverloadBlocks { get; set; }
}
