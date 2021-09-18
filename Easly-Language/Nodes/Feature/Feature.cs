namespace BaseNode
{
    /// <summary>
    /// Represents any feature.
    /// </summary>
    public abstract class Feature : Node
    {
        /// <summary>
        /// Gets or sets the export to which this feature belongs.
        /// </summary>
        public virtual Identifier ExportIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the export type.
        /// </summary>
        public virtual ExportStatus Export { get; set; }
    }
}
