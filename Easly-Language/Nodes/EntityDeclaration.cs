namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents an entity declaration.
    /// /Doc/Nodes/EntityDeclaration.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class EntityDeclaration : Node
    {
        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the entity default value.
        /// </summary>
        public virtual IOptionalReference<Expression> DefaultValue { get; set; } = null!;
    }
}
