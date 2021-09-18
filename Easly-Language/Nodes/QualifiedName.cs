namespace BaseNode
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a path of names across references.
    /// /Doc/Nodes/QualifiedName.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class QualifiedName : Node
    {
        /// <summary>
        /// Gets or sets list of feature identifiers to follow to reach the destination feature.
        /// </summary>
        public virtual IList<Identifier> Path { get; set; } = null!;
    }
}
