namespace BaseNode;

/// <summary>
/// Represents a contraint in a generic definition.
/// /Doc/Nodes/Constraint.md explains the semantic.
/// </summary>
[System.Serializable]
public class Constraint : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Constraint()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ParentType = default!;
        RenameBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Constraint"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parentType">The constraint type.</param>
    /// <param name="renameBlocks">The statements to use for this constraint.</param>
    internal Constraint(Document documentation, ObjectType parentType, IBlockList<Rename> renameBlocks)
        : base(documentation)
    {
        ParentType = parentType;
        RenameBlocks = renameBlocks;
    }

    /// <summary>
    /// Gets or sets the constraint type.
    /// </summary>
    public virtual ObjectType ParentType { get; set; }

    /// <summary>
    /// Gets or sets rename the statements to use for this constraint.
    /// </summary>
    public virtual IBlockList<Rename> RenameBlocks { get; set; }
}
