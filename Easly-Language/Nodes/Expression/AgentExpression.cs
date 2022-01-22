namespace BaseNode;

using Easly;

/// <summary>
/// Represents an agent expression.
/// /Doc/Nodes/Expression/AgentExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class AgentExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public AgentExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Delegated = default!;
        BaseType = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="AgentExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="delegated">The feature this agent represents.</param>
    /// <param name="baseType">The type where to find the feature.</param>
    internal AgentExpression(Document documentation, Identifier delegated, IOptionalReference<ObjectType> baseType)
        : base(documentation)
    {
        Delegated = delegated;
        BaseType = baseType;
    }

    /// <summary>
    /// Gets or sets the feature this agent represents.
    /// </summary>
    public virtual Identifier Delegated { get; set; }

    /// <summary>
    /// Gets or sets the type where to find the feature.
    /// </summary>
    public virtual IOptionalReference<ObjectType> BaseType { get; set; }
}
