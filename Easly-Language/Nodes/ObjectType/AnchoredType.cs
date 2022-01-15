namespace BaseNode;

/// <summary>
/// Represents an anchored type.
/// /Doc/Nodes/Type/AnchoredType.md explains the semantic.
/// </summary>
[System.Serializable]
public class AnchoredType : ObjectType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnchoredType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="anchoredName">How the type is shared.</param>
    /// <param name="anchorKind">Whether the anchor is at declaration or creation.</param>
    internal AnchoredType(Document documentation, QualifiedName anchoredName, AnchorKinds anchorKind)
        : base(documentation)
    {
        AnchoredName = anchoredName;
        AnchorKind = anchorKind;
    }

    /// <summary>
    /// Gets or sets the variable the type is anchored to.
    /// </summary>
    public virtual QualifiedName AnchoredName { get; set; }

    /// <summary>
    /// Gets or sets whether the anchor is at declaration or creation.
    /// </summary>
    public virtual AnchorKinds AnchorKind { get; set; }
}
