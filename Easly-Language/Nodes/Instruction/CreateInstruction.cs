namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an object creation instruction.
    /// /Doc/Nodes/Instruction/CreateInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CreateInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the created object.
        /// </summary>
        public virtual Identifier EntityIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the routine to call when creating.
        /// </summary>
        public virtual Identifier CreationRoutineIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the target processor.
        /// </summary>
        public virtual IOptionalReference<QualifiedName> Processor { get; set; } = null!;
    }
}
