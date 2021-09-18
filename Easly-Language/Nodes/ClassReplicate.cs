namespace BaseNode
{
    /// <summary>
    /// Represents a class replicate.
    /// /Doc/Nodes/ClassReplicate.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ClassReplicate : Node
    {
        /// <summary>
        /// Gets or sets the class replicate name.
        /// </summary>
        public virtual Name ReplicateName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the patterns.
        /// </summary>
        public virtual IBlockList<Pattern> PatternBlocks { get; set; } = null!;
    }
}
