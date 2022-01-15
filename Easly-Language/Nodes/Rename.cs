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
        /// Initializes a new instance of the <see cref="Rename"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="sourceIdentifier">The feature to rename.</param>
        /// <param name="destinationIdentifier">The new name.</param>
        internal Rename(Document documentation, Identifier sourceIdentifier, Identifier destinationIdentifier)
            : base(documentation)
        {
            SourceIdentifier = sourceIdentifier;
            DestinationIdentifier = destinationIdentifier;
        }

        /// <summary>
        /// Gets or sets the feature to rename.
        /// </summary>
        public virtual Identifier SourceIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the new name.
        /// </summary>
        public virtual Identifier DestinationIdentifier { get; set; }
    }
}
