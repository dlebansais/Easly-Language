namespace BaseNode
{
    /// <summary>
    /// Represents a type argument specified by assignment.
    /// /Doc/Nodes/TypeArgument/AssignmentTypeArgument.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AssignmentTypeArgument : TypeArgument
    {
        /// <summary>
        /// Gets or sets assigned parameter names.
        /// </summary>
        public virtual Identifier ParameterIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the source type.
        /// </summary>
        public virtual ObjectType Source { get; set; } = null!;
    }
}
