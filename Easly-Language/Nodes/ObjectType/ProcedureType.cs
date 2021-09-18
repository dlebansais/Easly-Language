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
        /// Gets or sets the base type.
        /// </summary>
        public virtual ObjectType BaseType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of overloads.
        /// </summary>
        public virtual IBlockList<CommandOverloadType> OverloadBlocks { get; set; } = null!;
    }
}
