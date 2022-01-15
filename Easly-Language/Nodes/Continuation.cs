namespace BaseNode
{
    /// <summary>
    /// Represents a continuation in a 'as long as' instruction.
    /// /Doc/Nodes/Continuation.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Continuation : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Continuation"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="instructions">The instructions in this continuation.</param>
        /// <param name="cleanupBlocks">The cleanup instructions.</param>
        internal Continuation(Document documentation, Scope instructions, IBlockList<Instruction> cleanupBlocks)
            : base(documentation)
        {
            Instructions = instructions;
            CleanupBlocks = cleanupBlocks;
        }

        /// <summary>
        /// Gets or sets the instructions in this continuation.
        /// </summary>
        public virtual Scope Instructions { get; set; }

        /// <summary>
        /// Gets or sets the cleanup instructions.
        /// </summary>
        public virtual IBlockList<Instruction> CleanupBlocks { get; set; }
    }
}
