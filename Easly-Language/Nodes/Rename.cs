#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IRename : INode
    {
        IIdentifier SourceIdentifier { get; }
        IIdentifier DestinationIdentifier { get; }
    }

    [System.Serializable]
    public class Rename : Node, IRename
    {
        public virtual IIdentifier SourceIdentifier { get; set; }
        public virtual IIdentifier DestinationIdentifier { get; set; }
    }
}
