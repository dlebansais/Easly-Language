namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an agent expression.
    /// /Doc/Nodes/Expression/AgentExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AgentExpression : Expression
    {
        /// <summary>
        /// Gets or sets the feature this agent represents.
        /// </summary>
        public virtual Identifier Delegated { get; set; } = null!;

        /// <summary>
        /// Gets or sets the type where to find the feature.
        /// </summary>
        public virtual IOptionalReference<ObjectType> BaseType { get; set; } = null!;
    }
}
