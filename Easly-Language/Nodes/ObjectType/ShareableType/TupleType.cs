namespace BaseNode;

/// <summary>
/// Represents a tuple type.
/// /Doc/Nodes/Type/TupleType.md explains the semantic.
/// </summary>
[System.Serializable]
public class TupleType : ShareableType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TupleType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="sharing">How the type is shared.</param>
    /// <param name="entityDeclarationBlocks">The list of elements in the tuple.</param>
    internal TupleType(Document documentation, SharingType sharing, IBlockList<EntityDeclaration> entityDeclarationBlocks)
        : base(documentation, sharing)
    {
        EntityDeclarationBlocks = entityDeclarationBlocks;
    }

    /// <summary>
    /// Gets or sets the list of elements in the tuple.
    /// </summary>
    public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; }
}
