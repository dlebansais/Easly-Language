namespace BaseNode
{
    /// <summary>
    /// Represents a tuple type.
    /// /Doc/Nodes/Type/TupleType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class TupleType : ShareableType
    {
        /// <summary>
        /// Gets or sets how the type is shared.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; } = null!;
    }
}
