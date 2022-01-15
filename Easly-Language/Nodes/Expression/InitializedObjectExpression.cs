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
        /// Initializes a new instance of the <see cref="InitializedObjectExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="classIdentifier">The class name.</param>
        /// <param name="assignmentBlocks">Initialization values.</param>
        internal InitializedObjectExpression(Document documentation, Identifier classIdentifier, IBlockList<AssignmentArgument> assignmentBlocks)
            : base(documentation)
        {
            ClassIdentifier = classIdentifier;
            AssignmentBlocks = assignmentBlocks;
        }

        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public virtual Identifier ClassIdentifier { get; set; }

        /// <summary>
        /// Gets or sets initialization values.
        /// </summary>
        public virtual IBlockList<AssignmentArgument> AssignmentBlocks { get; set; }
    }
}
