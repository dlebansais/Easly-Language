namespace BaseNode;

using Easly;

/// <summary>
/// Represents an agent expression.
/// /Doc/Nodes/Expression/AgentExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class AgentExpression : Expression
{
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
