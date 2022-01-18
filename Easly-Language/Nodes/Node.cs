namespace BaseNode;

/// <summary>
/// Represents any node.
/// </summary>
[System.Serializable]
public abstract class Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    public Node(Document documentation)
    {
        Documentation = documentation;
    }

    /// <summary>
    /// Gets or sets the node documentation.
    /// </summary>
    public virtual Document Documentation { get; set; }
}
