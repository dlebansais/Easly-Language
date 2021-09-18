namespace BaseNode
{
    /// <summary>
    /// Represents the instruction releasing a reference.
    /// /Doc/Nodes/Instruction/ReleaseInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ReleaseInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the released reference.
        /// </summary>
        public virtual QualifiedName EntityName { get; set; } = null!;
    }
}
