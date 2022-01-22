namespace BaseNode;

/// <summary>
/// Represents a class constant expression.
/// /Doc/Nodes/Expression/ClassConstantExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class ClassConstantExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ClassConstantExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ClassIdentifier = default!;
        ConstantIdentifier = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ClassConstantExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="classIdentifier">The name of the class where to find the constant.</param>
    /// <param name="constantIdentifier">The constant name.</param>
    internal ClassConstantExpression(Document documentation, Identifier classIdentifier, Identifier constantIdentifier)
        : base(documentation)
    {
        ClassIdentifier = classIdentifier;
        ConstantIdentifier = constantIdentifier;
    }

    /// <summary>
    /// Gets or sets the name of the class where to find the constant.
    /// </summary>
    public virtual Identifier ClassIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the constant name.
    /// </summary>
    public virtual Identifier ConstantIdentifier { get; set; }
}
