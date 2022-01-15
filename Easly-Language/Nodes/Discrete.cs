namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a discrete value.
    /// /Doc/Nodes/Discrete.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Discrete : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Discrete"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="entityName">The discrete's name.</param>
        /// <param name="numericValue">The discrete's value.</param>
        internal Discrete(Document documentation, Name entityName, IOptionalReference<Expression> numericValue)
            : base(documentation)
        {
            EntityName = entityName;
            NumericValue = numericValue;
        }

        /// <summary>
        /// Gets or sets the discrete's name.
        /// </summary>
        public virtual Name EntityName { get; set; }

        /// <summary>
        /// Gets or sets the discrete's value.
        /// </summary>
        public virtual IOptionalReference<Expression> NumericValue { get; set; }
    }
}
