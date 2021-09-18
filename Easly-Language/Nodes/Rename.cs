namespace BaseNode
{
    /// <summary>
    /// Represents a rename statement.
    /// /Doc/Nodes/Rename.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Rename : Node
    {
        /// <summary>
        /// Gets or sets the feature to rename.
        /// </summary>
        public virtual Identifier SourceIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets the new name.
        /// </summary>
        public virtual Identifier DestinationIdentifier { get; set; } = null!;
    }
}
