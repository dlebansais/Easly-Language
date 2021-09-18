namespace BaseNode
{
    /// <summary>
    /// Represents the instruction throwing an exception.
    /// /Doc/Nodes/Instruction/ThrowInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ThrowInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        public virtual ObjectType ExceptionType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the creation routine to use.
        /// </summary>
        public virtual Identifier CreationRoutine { get; set; } = null!;

        /// <summary>
        /// Gets or sets call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;
    }
}
