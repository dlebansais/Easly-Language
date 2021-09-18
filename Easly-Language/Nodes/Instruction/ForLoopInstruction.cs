namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a loop instruction.
    /// /Doc/Nodes/Instruction/ForLoopInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ForLoopInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets loop variables.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to initialize the loop.
        /// </summary>
        public virtual IBlockList<Instruction> InitInstructionBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the expression to continue.
        /// </summary>
        public virtual Expression WhileCondition { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions within the loop.
        /// </summary>
        public virtual IBlockList<Instruction> LoopInstructionBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions for the next iteration of the loop.
        /// </summary>
        public virtual IBlockList<Instruction> IterationInstructionBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the loop invariants.
        /// </summary>
        public virtual IBlockList<Assertion> InvariantBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the loop variant.
        /// </summary>
        public virtual IOptionalReference<Expression> Variant { get; set; } = null!;
    }
}
