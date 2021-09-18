namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an expression inspection instruction.
    /// /Doc/Nodes/Instruction/InspectInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class InspectInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the inspected expression.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of cases.
        /// </summary>
        public virtual IBlockList<With> WithBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute if none of the cases matched.
        /// </summary>
        public virtual IOptionalReference<Scope> ElseInstructions { get; set; } = null!;
    }
}
