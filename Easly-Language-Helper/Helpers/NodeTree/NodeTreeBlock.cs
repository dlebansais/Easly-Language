namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;
    using Contracts;

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
        internal NodeTreeBlock(Pattern replicationPattern, Identifier sourceIdentifier, IReadOnlyList<Node> nodeList)
        {
            Contract.RequireNotNull(replicationPattern, out Pattern ReplicationPattern);
            Contract.RequireNotNull(sourceIdentifier, out Identifier SourceIdentifier);
            Contract.RequireNotNull(nodeList, out IReadOnlyList<Node> NodeList);

            Debug.Assert(NodeList.Count > 0);

            this.ReplicationPattern = ReplicationPattern;
            this.SourceIdentifier = SourceIdentifier;
            this.NodeList = NodeList;
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
