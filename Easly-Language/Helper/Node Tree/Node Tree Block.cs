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
        public NodeTreeBlock(IPattern ReplicationPattern, IIdentifier SourceIdentifier, List<INode> NodeList)
        {
            Debug.Assert(NodeList.Count > 0);

            this.ReplicationPattern = ReplicationPattern;
            this.SourceIdentifier = SourceIdentifier;
            this.NodeList = NodeList.AsReadOnly();
        }
        #endregion

        #region Properties
        public IPattern ReplicationPattern { get; private set; }
        public IIdentifier SourceIdentifier { get; private set; }
        public IReadOnlyList<INode> NodeList { get; private set; }
        #endregion
    }
}
