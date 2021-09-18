namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a branching instruction.
    /// /Doc/Nodes/Instruction/IfThenElseInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class IfThenElseInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the list of conditionals.
        /// </summary>
        public virtual IBlockList<Conditional> ConditionalBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute if all conditionals were false.
        /// </summary>
        public virtual IOptionalReference<Scope> ElseInstructions { get; set; } = null!;
    }
}
