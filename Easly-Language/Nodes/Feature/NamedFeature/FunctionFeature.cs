namespace BaseNode
{
    /// <summary>
    /// Represents a function feature.
    /// /Doc/Nodes/Feature/FunctionFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class FunctionFeature : NamedFeature
    {
        /// <summary>
        /// Gets or sets whether this function is executed only once.
        /// </summary>
        public virtual OnceChoice Once { get; set; }

        /// <summary>
        /// Gets or sets the list of overloads.
        /// </summary>
        public virtual IBlockList<QueryOverload> OverloadBlocks { get; set; } = null!;
    }
}
