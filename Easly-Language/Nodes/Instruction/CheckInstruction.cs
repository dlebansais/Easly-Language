namespace BaseNode
{
    /// <summary>
    /// Represents a check instruction.
    /// /Doc/Nodes/Instruction/CheckInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CheckInstruction : Instruction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckInstruction"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="booleanExpression">The value to check.</param>
        internal CheckInstruction(Document documentation, Expression booleanExpression)
            : base(documentation)
        {
            BooleanExpression = booleanExpression;
        }

        /// <summary>
        /// Gets or sets the value to check.
        /// </summary>
        public virtual Expression BooleanExpression { get; set; }
    }
}
