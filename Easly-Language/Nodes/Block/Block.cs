namespace BaseNode
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IBlock
    {
        IDocument Documentation { get; }
        IList NodeList { get; }
        ReplicationStatus Replication { get; }
        IPattern ReplicationPattern { get; }
        IIdentifier SourceIdentifier { get; }
    }

    public interface IBlock<TNodeInterface, TNode>
        where TNodeInterface : class, INode
        where TNode : Node, TNodeInterface
    {
        IDocument Documentation { get; }
        IList<TNodeInterface> NodeList { get; }
        ReplicationStatus Replication { get; }
        IPattern ReplicationPattern { get; }
        IIdentifier SourceIdentifier { get; }
    }

    [System.Serializable]
    public class Block<TNodeInterface, TNode> : IBlock<TNodeInterface, TNode>, IBlock
        where TNodeInterface : class, INode
        where TNode : Node, TNodeInterface
    {
        public virtual IDocument Documentation { get; set; }
        public virtual IList<TNodeInterface> NodeList { get; set; }
        IList IBlock.NodeList { get { return NodeList as IList; } }
        public virtual ReplicationStatus Replication { get; set; }
        public virtual IPattern ReplicationPattern { get; set; }
        public virtual IIdentifier SourceIdentifier { get; set; }
    }
}
