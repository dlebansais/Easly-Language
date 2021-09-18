namespace BaseNode
{
    /// <summary>
    /// Represents a creation feature.
    /// /Doc/Nodes/Feature/CreationFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CreationFeature : NamedFeature
    {
        /// <summary>
        /// Gets or sets the list of overloads.
        /// </summary>
        public virtual IBlockList<CommandOverload> OverloadBlocks { get; set; } = null!;
    }
}
