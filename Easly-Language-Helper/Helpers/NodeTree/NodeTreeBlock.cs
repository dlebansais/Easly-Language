#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public class NodeTreeBlock
    {
        #region Init
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
        public Pattern ReplicationPattern { get; }
        public Identifier SourceIdentifier { get; }
        public IReadOnlyList<Node> NodeList { get; }
        #endregion
    }
}
