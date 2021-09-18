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
        /// Gets or sets the value to check.
        /// </summary>
        public virtual Expression BooleanExpression { get; set; } = null!;
    }
}
