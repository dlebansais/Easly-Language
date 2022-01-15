namespace BaseNode
{
    /// <summary>
    /// Represents a clone expression.
    /// /Doc/Nodes/Expression/CloneOfExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CloneOfExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloneOfExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="type">The clone type.</param>
        /// <param name="source">The cloned source.</param>
        internal CloneOfExpression(Document documentation, CloneType type, Expression source)
            : base(documentation)
        {
            Type = type;
            Source = source;
        }

        /// <summary>
        /// Gets or sets the clone type.
        /// </summary>
        public virtual CloneType Type { get; set; }

        /// <summary>
        /// Gets or sets the cloned source.
        /// </summary>
        public virtual Expression Source { get; set; }
    }
}
