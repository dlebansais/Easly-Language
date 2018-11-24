using System.Collections;
using System.Collections.Generic;

namespace BaseNode
{
    public interface IBlock
    {
        IDocument Documentation { get; }
        ReplicationStatus Replication { get; }
        IPattern ReplicationPattern { get; }
        IIdentifier SourceIdentifier { get; }
    }

    public interface IBlock<IN, N> : IBlock
        where IN : class, INode
        where N : Node, IN
    {
        IList<IN> NodeList { get; }
    }

    [System.Serializable]
    public class Block<IN, N> : IBlock<IN, N>
        where IN : class, INode
        where N : Node, IN
    {
        public virtual IDocument Documentation { get; set; }
        public virtual IList<IN> NodeList { get; set; }
        public virtual ReplicationStatus Replication { get; set; }
        public virtual IPattern ReplicationPattern { get; set; }
        public virtual IIdentifier SourceIdentifier { get; set; }
    }
}
