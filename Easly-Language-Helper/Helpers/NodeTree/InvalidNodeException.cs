namespace BaseNodeHelper
{
    using System;
    using BaseNode;

    /// <summary>
    /// Defines an exception thrown when a node is found invalid.
    /// </summary>
    public class InvalidNodeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidNodeException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="rootNode">The node containing the invalid node.</param>
        /// <param name="invalidNode">The invalid node.</param>
        internal InvalidNodeException(string message, Node rootNode, Node invalidNode)
            : base($"Invalid Node: {message}")
        {
            RootNode = rootNode;
            InvalidNode = invalidNode;
        }

        /// <summary>
        /// Gets the node containing the invalid node.
        /// </summary>
        public Node RootNode { get; }

        /// <summary>
        /// Gets the invalid node.
        /// </summary>
        public Node InvalidNode { get; }
    }
}
