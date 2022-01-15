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
        /// Initializes a new instance of the <see cref="Conditional"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="booleanExpression">The command parameters.</param>
        /// <param name="instructions">Whether the command accepts extra parameters.</param>
        internal Conditional(Document documentation, Expression booleanExpression, Scope instructions)
            : base(documentation)
        {
            BooleanExpression = booleanExpression;
            Instructions = instructions;
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        public virtual Expression BooleanExpression { get; set; }

        /// <summary>
        /// Gets or sets instructions to execute if the condition is true.
        /// </summary>
        public virtual Scope Instructions { get; set; }
    }
}
