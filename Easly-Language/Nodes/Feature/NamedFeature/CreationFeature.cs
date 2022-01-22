namespace BaseNode;

/// <summary>
/// Represents a creation feature.
/// /Doc/Nodes/Feature/CreationFeature.md explains the semantic.
/// </summary>
[System.Serializable]
public class CreationFeature : NamedFeature
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public CreationFeature()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!, default!, default!, default!)
    {
        OverloadBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="CreationFeature"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exportIdentifier">The export to which this feature belongs.</param>
    /// <param name="export">The export type.</param>
    /// <param name="entityName">The creation feature name.</param>
    /// <param name="overloadBlocks">The list of overloads.</param>
    internal CreationFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, IBlockList<CommandOverload> overloadBlocks)
        : base(documentation, exportIdentifier, export, entityName)
    {
        OverloadBlocks = overloadBlocks;
    }

    /// <summary>
    /// Gets or sets the list of overloads.
    /// </summary>
    public virtual IBlockList<CommandOverload> OverloadBlocks { get; set; }
}
