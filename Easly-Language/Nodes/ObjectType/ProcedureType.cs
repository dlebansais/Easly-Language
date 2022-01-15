namespace BaseNode
{
    /// <summary>
    /// Represents a procedure type.
    /// /Doc/Nodes/Type/ProcedureType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ProcedureType : ObjectType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureType"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="baseType">The base type.</param>
        /// <param name="overloadBlocks">The list of overloads.</param>
        internal ProcedureType(Document documentation, ObjectType baseType, IBlockList<CommandOverloadType> overloadBlocks)
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
        /// Gets or sets the list of overloads.
        /// </summary>
        public virtual IBlockList<CommandOverloadType> OverloadBlocks { get; set; }
    }
}
