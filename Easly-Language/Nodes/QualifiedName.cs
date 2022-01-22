namespace BaseNode;

using System.Collections.Generic;

/// <summary>
/// Represents a path of names across references.
/// /Doc/Nodes/QualifiedName.md explains the semantic.
/// </summary>
[System.Serializable]
public class QualifiedName : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public QualifiedName()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Path = default!;
    }
#endif
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
