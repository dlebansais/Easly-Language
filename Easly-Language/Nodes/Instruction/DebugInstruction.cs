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
        /// Initializes a new instance of the <see cref="DebugInstruction"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="instructions">Instructions to execute conditionally.</param>
        internal DebugInstruction(Document documentation, Scope instructions)
            : base(documentation)
        {
            Instructions = instructions;
        }

        /// <summary>
        /// Gets or sets instructions to execute conditionally.
        /// </summary>
        public virtual Scope Instructions { get; set; }
    }
}
