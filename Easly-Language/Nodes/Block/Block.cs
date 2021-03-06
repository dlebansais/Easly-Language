#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
        IList IBlock.NodeList { get { return (IList)NodeList; } }
        public virtual ReplicationStatus Replication { get; set; }
        public virtual IPattern ReplicationPattern { get; set; }
        public virtual IIdentifier SourceIdentifier { get; set; }
    }
}
