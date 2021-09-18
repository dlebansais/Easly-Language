namespace BaseNode
{
    /// <summary>
    /// Represents the 'with' part of the 'inspect' instruction.
    /// /Doc/Nodes/With.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class With : Node
    {
        /// <summary>
        /// Gets or sets range blocks.
        /// </summary>
        public virtual IBlockList<Range> RangeBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions for this case.
        /// </summary>
        public virtual Scope Instructions { get; set; } = null!;
    }
}
