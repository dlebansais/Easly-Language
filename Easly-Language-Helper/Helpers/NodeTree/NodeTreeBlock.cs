namespace BaseNodeHelper
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BaseNode;

    public interface INodeTreeBlock
    {
        IPattern ReplicationPattern { get; }
        IIdentifier SourceIdentifier { get; }
        IReadOnlyList<INode> NodeList { get; }
    }

    public class NodeTreeBlock : INodeTreeBlock
    {
        #region Init
        public NodeTreeBlock(IPattern replicationPattern, IIdentifier sourceIdentifier, IReadOnlyList<INode> nodeList)
        {
            Debug.Assert(nodeList.Count > 0);

            ReplicationPattern = replicationPattern;
            SourceIdentifier = sourceIdentifier;
            NodeList = nodeList;
        }
        #endregion

        #region Properties
        public IPattern ReplicationPattern { get; }
        public IIdentifier SourceIdentifier { get; }
        public IReadOnlyList<INode> NodeList { get; }
        #endregion
    }
}
