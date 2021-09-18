namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents the 'as long as' instruction.
    /// /Doc/Nodes/Instruction/AsLongAsInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AsLongAsInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the condition evaluated to continue.
        /// </summary>
        public virtual Expression ContinueCondition { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of continuations.
        /// </summary>
        public virtual IBlockList<Continuation> ContinuationBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute when we don't continue.
        /// </summary>
        public virtual IOptionalReference<Scope> ElseInstructions { get; set; } = null!;
    }
}
