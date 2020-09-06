#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using System.Collections.Generic;

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
