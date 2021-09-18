namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents the instruction looping over collections.
    /// /Doc/Nodes/Instruction/OverLoopInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class OverLoopInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the collection(s) to loop over.
        /// </summary>
        public virtual Expression OverList { get; set; } = null!;

        /// <summary>
        /// Gets or sets created indexes for each collection.
        /// </summary>
        public virtual IBlockList<Name> IndexerBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the iteration is parallel or recursive.
        /// </summary>
        public virtual IterationType Iteration { get; set; }

        /// <summary>
        /// Gets or sets instructions within the loop.
        /// </summary>
        public virtual Scope LoopInstructions { get; set; } = null!;

        /// <summary>
        /// Gets or sets the identifier of a variable to check and exit if true.
        /// </summary>
        public virtual IOptionalReference<Identifier> ExitEntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the loop invariants.
        /// </summary>
        public virtual IBlockList<Assertion> InvariantBlocks { get; set; } = null!;
    }
}
