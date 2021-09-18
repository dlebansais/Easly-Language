namespace BaseNode
{
    /// <summary>
    /// Represents a type with generics.
    /// /Doc/Nodes/Type/GenericType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class GenericType : ShareableType
    {
        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        public virtual Identifier ClassIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of generic parameters.
        /// </summary>
        public virtual IBlockList<TypeArgument> TypeArgumentBlocks { get; set; } = null!;
    }
}
