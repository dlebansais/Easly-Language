namespace BaseNode;

/// <summary>
/// Represents a type argument specified by assignment.
/// /Doc/Nodes/TypeArgument/AssignmentTypeArgument.md explains the semantic.
/// </summary>
[System.Serializable]
public class AssignmentTypeArgument : TypeArgument
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public AssignmentTypeArgument()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ParameterIdentifier = default!;
        Source = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentTypeArgument"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parameterIdentifier">The assigned parameter name.</param>
    /// <param name="source">The source type.</param>
    internal AssignmentTypeArgument(Document documentation, Identifier parameterIdentifier, ObjectType source)
        : base(documentation)
    {
        ParameterIdentifier = parameterIdentifier;
        Source = source;
    }

    /// <summary>
    /// Gets or sets the assigned parameter name.
    /// </summary>
    public virtual Identifier ParameterIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the source type.
    /// </summary>
    public virtual ObjectType Source { get; set; }
}
