namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using BaseNode;

    /// <summary>
    /// Represents a block of nodes in the program tree.
    /// </summary>
    public class NodeTreeBlock
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeTreeBlock"/> class.
        /// </summary>
        /// <param name="replicationPattern">The replication pattern.</param>
        /// <param name="sourceIdentifier">The source identifier.</param>
        /// <param name="nodeList">The list of nodes.</param>
        public NodeTreeBlock(Pattern replicationPattern, Identifier sourceIdentifier, IReadOnlyList<Node> nodeList)
        {
            if (nodeList == null) throw new ArgumentNullException(nameof(nodeList));
            if (nodeList.Count == 0) throw new ArgumentException($"{nameof(nodeList)} must have at least one node");

            ReplicationPattern = replicationPattern;
            SourceIdentifier = sourceIdentifier;
            NodeList = nodeList;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the replication pattern.
        /// </summary>
        public Pattern ReplicationPattern { get; }

        /// <summary>
        /// Gets the source identifier.
        /// </summary>
        public Identifier SourceIdentifier { get; }

        /// <summary>
        /// Gets the list of nodes.
        /// </summary>
        public IReadOnlyList<Node> NodeList { get; }
        #endregion
    }
}
