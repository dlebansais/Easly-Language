namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents the call of a precursor.
    /// /Doc/Nodes/Instruction/PrecursorInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PrecursorInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the type where to find the precursor.
        /// </summary>
        public virtual IOptionalReference<ObjectType> AncestorType { get; set; } = null!;

        /// <summary>
        /// Gets or sets call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;
    }
}
