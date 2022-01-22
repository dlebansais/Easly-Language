namespace BaseNode;

/// <summary>
/// Represents the expression designating an object as new.
/// /Doc/Nodes/Expression/NewExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class NewExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public NewExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Object = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="NewExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="object">The name path to the new object.</param>
    internal NewExpression(Document documentation, QualifiedName @object)
        : base(documentation)
    {
        Object = @object;
    }

    /// <summary>
    /// Gets or sets the name path to the new object.
    /// </summary>
    public virtual QualifiedName Object { get; set; }
}
