namespace BaseNode
{
    /// <summary>
    /// Represents an argument specified by assignment.
    /// /Doc/Nodes/Argument/AssignmentArgument.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AssignmentArgument : Argument
    {
        /// <summary>
        /// Gets or sets assigned parameter identifiers.
        /// </summary>
        public virtual IBlockList<Identifier> ParameterBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the argument source.
        /// </summary>
        public virtual Expression Source { get; set; } = null!;
    }
}
