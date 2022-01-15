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
        /// Initializes a new instance of the <see cref="IfThenElseInstruction"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="conditionalBlocks">The list of conditionals.</param>
        /// <param name="elseInstructions">Instructions to execute if all conditionals were false.</param>
        internal IfThenElseInstruction(Document documentation, IBlockList<Conditional> conditionalBlocks, IOptionalReference<Scope> elseInstructions)
            : base(documentation)
        {
            ConditionalBlocks = conditionalBlocks;
            ElseInstructions = elseInstructions;
        }

        /// <summary>
        /// Gets or sets the list of conditionals.
        /// </summary>
        public virtual IBlockList<Conditional> ConditionalBlocks { get; set; }

        /// <summary>
        /// Gets or sets instructions to execute if all conditionals were false.
        /// </summary>
        public virtual IOptionalReference<Scope> ElseInstructions { get; set; }
    }
}
