namespace BaseNode;

/// <summary>
/// Represents an argument specified by assignment.
/// /Doc/Nodes/Argument/AssignmentArgument.md explains the semantic.
/// </summary>
[System.Serializable]
public class AssignmentArgument : Argument
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentArgument"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parameterBlocks">Assigned parameter identifiers.</param>
    /// <param name="source">The argument source.</param>
    internal AssignmentArgument(Document documentation, IBlockList<Identifier> parameterBlocks, Expression source)
        : base(documentation)
    {
        ParameterBlocks = parameterBlocks;
        Source = source;
    }

    /// <summary>
    /// Gets or sets assigned parameter identifiers.
    /// </summary>
    public virtual IBlockList<Identifier> ParameterBlocks { get; set; }

    /// <summary>
    /// Gets or sets the argument source.
    /// </summary>
    public virtual Expression Source { get; set; }
}
