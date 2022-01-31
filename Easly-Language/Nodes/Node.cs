namespace BaseNode;

/// <summary>
/// Represents any node.
/// </summary>
[System.Serializable]
public abstract class Node
{
    /// <summary>
    /// Gets the default <see cref="Node"/> object.
    /// </summary>
    public static Node Default { get; } = new DefaultNode();

    /// <summary>
    /// Represents the definition of a default node.
    /// </summary>
    protected class DefaultNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNode"/> class.
        /// </summary>
        public DefaultNode()
        {
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    protected Node()
    {
        Documentation = Document.Default;
    }

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
