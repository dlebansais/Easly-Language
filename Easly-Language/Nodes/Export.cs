namespace BaseNode
{
    /// <summary>
    /// Represents an export statement.
    /// /Doc/Nodes/Export.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Export : Node
    {
        /// <summary>
        /// Gets or sets the export name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets classes exported to by name.
        /// </summary>
        public virtual IBlockList<Identifier> ClassIdentifierBlocks { get; set; } = null!;
    }
}
