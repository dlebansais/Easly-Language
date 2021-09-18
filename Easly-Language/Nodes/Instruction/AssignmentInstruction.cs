namespace BaseNode
{
    /// <summary>
    /// Represents an assignment instruction.
    /// /Doc/Nodes/Instruction/AssignmentInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AssignmentInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets assigned variables.
        /// </summary>
        public virtual IBlockList<QualifiedName> DestinationBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the value to assign.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
