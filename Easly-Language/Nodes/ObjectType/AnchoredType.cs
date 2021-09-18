namespace BaseNode
{
    /// <summary>
    /// Represents an anchored type.
    /// /Doc/Nodes/Type/AnchoredType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AnchoredType : ObjectType
    {
        /// <summary>
        /// Gets or sets the variable the type is anchored to.
        /// </summary>
        public virtual QualifiedName AnchoredName { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the anchor is at declaration or creation.
        /// </summary>
        public virtual AnchorKinds AnchorKind { get; set; }
    }
}
