namespace BaseNode
{
    /// <summary>
    /// Represents an expression initializing an object.
    /// /Doc/Nodes/Expression/InitializedObjectExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class InitializedObjectExpression : Expression
    {
        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public virtual Identifier ClassIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets initialization values.
        /// </summary>
        public virtual IBlockList<AssignmentArgument> AssignmentBlocks { get; set; } = null!;
    }
}
