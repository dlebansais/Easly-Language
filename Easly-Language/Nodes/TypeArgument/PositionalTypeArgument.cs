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
        /// Gets or sets the argument type.
        /// </summary>
        public virtual ObjectType Source { get; set; } = null!;
    }
}
