namespace BaseNode
{
    /// <summary>
    /// Represents a conditional in a 'if' instruction.
    /// /Doc/Nodes/Conditional.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Conditional : Node
    {
        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        public virtual Expression BooleanExpression { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute if the condition is true.
        /// </summary>
        public virtual Scope Instructions { get; set; } = null!;
    }
}
