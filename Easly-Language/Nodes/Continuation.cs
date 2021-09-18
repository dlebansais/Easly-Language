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
        /// Gets or sets instructions in this continuation.
        /// </summary>
        public virtual Scope Instructions { get; set; } = null!;

        /// <summary>
        /// Gets or sets cleanup instructions.
        /// </summary>
        public virtual IBlockList<Instruction> CleanupBlocks { get; set; } = null!;
    }
}
