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
        /// Initializes a new instance of the <see cref="FunctionType"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="baseType">The base type.</param>
        /// <param name="overloadBlocks">The list of overload types.</param>
        internal FunctionType(Document documentation, ObjectType baseType, IBlockList<QueryOverloadType> overloadBlocks)
            : base(documentation)
        {
            BaseType = baseType;
            OverloadBlocks = overloadBlocks;
        }

        /// <summary>
        /// Gets or sets the base type.
        /// </summary>
        public virtual ObjectType BaseType { get; set; }

        /// <summary>
        /// Gets or sets the list of overload types.
        /// </summary>
        public virtual IBlockList<QueryOverloadType> OverloadBlocks { get; set; }
    }
}
