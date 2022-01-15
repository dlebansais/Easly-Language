namespace BaseNode
{
    /// <summary>
    /// Represents a type argument specified by position.
    /// /Doc/Nodes/TypeArgument/PositionalTypeArgument.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PositionalTypeArgument : TypeArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionalTypeArgument"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="source">The argument type.</param>
        internal PositionalTypeArgument(Document documentation, ObjectType source)
            : base(documentation)
        {
            Source = source;
        }

        /// <summary>
        /// Gets or sets the argument type.
        /// </summary>
        public virtual ObjectType Source { get; set; }
    }
}
