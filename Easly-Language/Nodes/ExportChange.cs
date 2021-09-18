namespace BaseNode
{
    /// <summary>
    /// Represents a change in the export status of an inherited feature.
    /// /Doc/Nodes/ExportChange.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ExportChange : Node
    {
        /// <summary>
        /// Gets or sets the modified export.
        /// </summary>
        public virtual Identifier ExportIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the new class names.
        /// </summary>
        public virtual IBlockList<Identifier> IdentifierBlocks { get; set; } = null!;
    }
}
