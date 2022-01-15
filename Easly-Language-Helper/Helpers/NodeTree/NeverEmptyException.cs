namespace BaseNodeHelper;

using System;
using BaseNode;

/// <summary>
/// Defines an exception thrown when attempting to empty a list that must not be empty.
/// </summary>
public class NeverEmptyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NeverEmptyException"/> class.
    /// </summary>
    /// <param name="node">The node with the list property.</param>
    /// <param name="propertyName">The property name.</param>
    internal NeverEmptyException(Node node, string propertyName)
        : base($"Collection '{propertyName}' in '{node.GetType()}' must not be empty")
    {
        Node = node;
        PropertyName = propertyName;
    }

    /// <summary>
    /// Gets the node with the list property.
    /// </summary>
    public Node Node { get; }

    /// <summary>
    /// Gets the property name.
    /// </summary>
    public string PropertyName { get; }
}
