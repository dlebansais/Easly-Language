namespace BaseNode;

/// <summary>
/// Represents an expression initializing an object.
/// /Doc/Nodes/Expression/InitializedObjectExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class InitializedObjectExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public InitializedObjectExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ClassIdentifier = default!;
        AssignmentBlocks = default!;
    }
#endif
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
