#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IBlock
    {
        Document Documentation { get; }
        IList NodeList { get; }
        ReplicationStatus Replication { get; }
        Pattern ReplicationPattern { get; }
        Identifier SourceIdentifier { get; }
    }

    [System.Serializable]
    public class Block<TNode> : IBlock
        where TNode : Node
    {
        public virtual Document Documentation { get; set; }
        public virtual IList<TNode> NodeList { get; set; }
        IList IBlock.NodeList { get { return (IList)NodeList; } }
        public virtual ReplicationStatus Replication { get; set; }
        public virtual Pattern ReplicationPattern { get; set; }
        public virtual Identifier SourceIdentifier { get; set; }
    }
}
