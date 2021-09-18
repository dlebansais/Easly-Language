namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents the assigment of the precursor of an indexer.
    /// /Doc/Nodes/Instruction/PrecursorIndexAssignmentInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PrecursorIndexAssignmentInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the type where to find the precursor.
        /// </summary>
        public virtual IOptionalReference<ObjectType> AncestorType { get; set; } = null!;

        /// <summary>
        /// Gets or sets call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the value to assign.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
