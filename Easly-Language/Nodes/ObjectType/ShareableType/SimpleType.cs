namespace BaseNode;

/// <summary>
/// Represents a simple type.
/// /Doc/Nodes/Type/SimpleType.md explains the semantic.
/// </summary>
[System.Serializable]
public class SimpleType : ShareableType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="sharing">How the type is shared.</param>
    /// <param name="classIdentifier">The class identifier.</param>
    internal SimpleType(Document documentation, SharingType sharing, Identifier classIdentifier)
        : base(documentation, sharing)
    {
        ClassIdentifier = classIdentifier;
    }

    /// <summary>
    /// Gets or sets the class identifier.
    /// </summary>
    public virtual Identifier ClassIdentifier { get; set; }
}
