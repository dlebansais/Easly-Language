namespace BaseNode
{
    /// <summary>
    /// Represents an index assignment instruction.
    /// /Doc/Nodes/Instruction/IndexAssignmentInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class IndexAssignmentInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the assigned variable.
        /// </summary>
        public virtual QualifiedName Destination { get; set; } = null!;

        /// <summary>
        /// Gets or sets call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the index in the destination.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
