using System.Collections.Generic;

namespace BaseNode
{
    public interface IGlobalReplicate : INode
    {
        IName ReplicateName { get; }
        IList<IPattern> Patterns { get; }
    }

    [System.Serializable]
    public class GlobalReplicate : Node, IGlobalReplicate
    {
        public virtual IName ReplicateName { get; set; }
        public virtual IList<IPattern> Patterns { get; set; }
    }
}
