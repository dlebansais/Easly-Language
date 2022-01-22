namespace BaseNode;

/// <summary>
/// Represents any argument (positional or assignment).
/// /Doc/Nodes/Argument/PositionalArgument.md explains the semantic.
/// </summary>
[System.Serializable]
public class PositionalArgument : Argument
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public PositionalArgument()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Source = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="PositionalArgument"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="source">The argument source.</param>
    internal PositionalArgument(Document documentation, Expression source)
        : base(documentation)
    {
        Source = source;
    }

    /// <summary>
    /// Gets or sets the argument source.
    /// </summary>
    public virtual Expression Source { get; set; }
}
