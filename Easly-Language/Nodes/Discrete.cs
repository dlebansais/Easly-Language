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
        /// Gets or sets the discrete's name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the discrete's value.
        /// </summary>
        public virtual IOptionalReference<Expression> NumericValue { get; set; } = null!;
    }
}
