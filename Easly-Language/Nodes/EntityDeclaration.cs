namespace BaseNode;

using Easly;

/// <summary>
/// Represents an entity declaration.
/// /Doc/Nodes/EntityDeclaration.md explains the semantic.
/// </summary>
[System.Serializable]
public class EntityDeclaration : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDeclaration"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityName">The entity name.</param>
    /// <param name="entityType">The entity type.</param>
    /// <param name="defaultValue">The entity default value.</param>
    internal EntityDeclaration(Document documentation, Name entityName, ObjectType entityType, IOptionalReference<Expression> defaultValue)
        : base(documentation)
    {
        EntityName = entityName;
        EntityType = entityType;
        DefaultValue = defaultValue;
    }

    /// <summary>
    /// Gets or sets the entity name.
    /// </summary>
    public virtual Name EntityName { get; set; }

    /// <summary>
    /// Gets or sets the entity type.
    /// </summary>
    public virtual ObjectType EntityType { get; set; }

    /// <summary>
    /// Gets or sets the entity default value.
    /// </summary>
    public virtual IOptionalReference<Expression> DefaultValue { get; set; }
}
