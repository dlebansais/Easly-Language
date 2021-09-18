namespace BaseNode
{
    /// <summary>
    /// Represents a simple type.
    /// /Doc/Nodes/Type/SimpleType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class SimpleType : ShareableType
    {
        /// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        public virtual Identifier ClassIdentifier { get; set; } = null!;
    }
}
