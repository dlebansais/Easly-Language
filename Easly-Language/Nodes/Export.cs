namespace BaseNode;

/// <summary>
/// Represents an export statement.
/// /Doc/Nodes/Export.md explains the semantic.
/// </summary>
[System.Serializable]
public class Export : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Export"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityName">The export name.</param>
    /// <param name="classIdentifierBlocks">The classes exported to by name.</param>
    internal Export(Document documentation, Name entityName, IBlockList<Identifier> classIdentifierBlocks)
        : base(documentation)
    {
        EntityName = entityName;
        ClassIdentifierBlocks = classIdentifierBlocks;
    }

    /// <summary>
    /// Gets or sets the export name.
    /// </summary>
    public virtual Name EntityName { get; set; }

    /// <summary>
    /// Gets or sets the classes exported to by name.
    /// </summary>
    public virtual IBlockList<Identifier> ClassIdentifierBlocks { get; set; }
}
