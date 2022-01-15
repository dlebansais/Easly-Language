namespace BaseNode;

/// <summary>
/// Represents any argument (positional or assignment).
/// /Doc/Nodes/Argument/PositionalArgument.md explains the semantic.
/// </summary>
[System.Serializable]
public class PositionalArgument : Argument
{
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
