namespace BaseNode
{
    /// <summary>
    /// Represents a function type.
    /// /Doc/Nodes/Type/FunctionType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class FunctionType : ObjectType
    {
        /// <summary>
        /// Gets or sets the base type.
        /// </summary>
        public virtual ObjectType BaseType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of overload types.
        /// </summary>
        public virtual IBlockList<QueryOverloadType> OverloadBlocks { get; set; } = null!;
    }
}
