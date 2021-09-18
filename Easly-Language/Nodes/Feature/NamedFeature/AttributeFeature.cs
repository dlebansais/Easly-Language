namespace BaseNode
{
    /// <summary>
    /// Represents an attribute feature.
    /// /Doc/Nodes/Feature/AttributeFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class AttributeFeature : NamedFeature
    {
        /// <summary>
        /// Gets or sets the attribute type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets attribute guaranties.
        /// </summary>
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; } = null!;
    }
}
