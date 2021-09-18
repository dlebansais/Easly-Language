namespace BaseNode
{
    /// <summary>
    /// Represents a debugging instruction.
    /// /Doc/Nodes/Instruction/DebugInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class DebugInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets instructions to execute conditionally.
        /// </summary>
        public virtual Scope Instructions { get; set; } = null!;
    }
}
