namespace BaseNode
{
    /// <summary>
    /// Represents an index assignment instruction.
    /// /Doc/Nodes/Instruction/IndexAssignmentInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class IndexAssignmentInstruction : Instruction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexAssignmentInstruction"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="destination">The assigned variable.</param>
        /// <param name="argumentBlocks">The call arguments.</param>
        /// <param name="source">The index in the destination.</param>
        internal IndexAssignmentInstruction(Document documentation, QualifiedName destination, IBlockList<Argument> argumentBlocks, Expression source)
            : base(documentation)
        {
            Destination = destination;
            ArgumentBlocks = argumentBlocks;
            Source = source;
        }

        /// <summary>
        /// Gets or sets the assigned variable.
        /// </summary>
        public virtual QualifiedName Destination { get; set; }

        /// <summary>
        /// Gets or sets the call arguments.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; }

        /// <summary>
        /// Gets or sets the index in the destination.
        /// </summary>
        public virtual Expression Source { get; set; }
    }
}
