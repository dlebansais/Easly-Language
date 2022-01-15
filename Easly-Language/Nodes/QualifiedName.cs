namespace BaseNode;

using System.Collections.Generic;

/// <summary>
/// Represents a path of names across references.
/// /Doc/Nodes/QualifiedName.md explains the semantic.
/// </summary>
[System.Serializable]
public class QualifiedName : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiedName"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="path">The list of feature identifiers to follow to reach the destination feature.</param>
    internal QualifiedName(Document documentation, IList<Identifier> path)
        : base(documentation)
    {
        Path = path;
    }

    /// <summary>
    /// Gets or sets the list of feature identifiers to follow to reach the destination feature.
    /// </summary>
    public virtual IList<Identifier> Path { get; set; }
}
