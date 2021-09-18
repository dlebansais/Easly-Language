namespace BaseNode
{
    /// <summary>
    /// Represents a command instruction.
    /// /Doc/Nodes/Instruction/CommandInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CommandInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the feature to call.
        /// </summary>
        public virtual QualifiedName Command { get; set; } = null!;

        /// <summary>
        /// Gets or sets call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;
    }
}
