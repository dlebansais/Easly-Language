using BaseNode;
using System.Collections.Generic;
using System.Diagnostics;

namespace BaseNodeHelper
{
    public interface INodeTreeBlock
    {
        IPattern ReplicationPattern { get; }
        IIdentifier SourceIdentifier { get; }
        IReadOnlyList<INode> NodeList { get; }
    }

    public class NodeTreeBlock : INodeTreeBlock
    {
        #region Init
        public NodeTreeBlock(IPattern replicationPattern, IIdentifier sourceIdentifier, List<INode> nodeList)
        {
            Debug.Assert(nodeList.Count > 0);

            ReplicationPattern = replicationPattern;
            SourceIdentifier = sourceIdentifier;
            NodeList = nodeList.AsReadOnly();
        }
        #endregion

        #region Properties
        public IPattern ReplicationPattern { get; private set; }
        public IIdentifier SourceIdentifier { get; private set; }
        public IReadOnlyList<INode> NodeList { get; private set; }
        #endregion
    }
}
