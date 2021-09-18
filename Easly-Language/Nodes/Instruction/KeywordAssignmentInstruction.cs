namespace BaseNode
{
    /// <summary>
    /// Represents the instruction assigning value to a keyword.
    /// /Doc/Nodes/Instruction/KeywordAssignmentInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class KeywordAssignmentInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the keyword to assign.
        /// </summary>
        public virtual Keyword Destination { get; set; }

        /// <summary>
        /// Gets or sets the assigned value.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
