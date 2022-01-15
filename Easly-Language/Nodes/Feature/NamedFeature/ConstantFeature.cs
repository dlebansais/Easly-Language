namespace BaseNode;

/// <summary>
/// Represents a constant feature.
/// /Doc/Nodes/Feature/ConstantFeature.md explains the semantic.
/// </summary>
[System.Serializable]
public class ConstantFeature : NamedFeature
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstantFeature"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exportIdentifier">The export to which this feature belongs.</param>
    /// <param name="export">The export type.</param>
    /// <param name="entityName">The constant name.</param>
    /// <param name="entityType">The constant type.</param>
    /// <param name="constantValue">Attribute guarantees.</param>
    internal ConstantFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, ObjectType entityType, Expression constantValue)
        : base(documentation, exportIdentifier, export, entityName)
    {
        EntityType = entityType;
        ConstantValue = constantValue;
    }

    /// <summary>
    /// Gets or sets the constant type.
    /// </summary>
    public virtual ObjectType EntityType { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public virtual Expression ConstantValue { get; set; }
}
