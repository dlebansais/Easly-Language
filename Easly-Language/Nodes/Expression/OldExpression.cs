namespace BaseNode;

/// <summary>
/// Represents the expression designating the value of an object before it was modified.
/// /Doc/Nodes/Expression/OldExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class OldExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public OldExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Query = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="OldExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="query">The name path to the object.</param>
    internal OldExpression(Document documentation, QualifiedName query)
        : base(documentation)
    {
        Query = query;
    }

    /// <summary>
    /// Gets or sets the name path to the object.
    /// </summary>
    public virtual QualifiedName Query { get; set; }
}
