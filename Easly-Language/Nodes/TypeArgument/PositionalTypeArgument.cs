namespace BaseNode;

/// <summary>
/// Represents a type argument specified by position.
/// /Doc/Nodes/TypeArgument/PositionalTypeArgument.md explains the semantic.
/// </summary>
[System.Serializable]
public class PositionalTypeArgument : TypeArgument
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public PositionalTypeArgument()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Source = default!;
    }
#endif
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
