#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IIdentifier : INode
    {
        string Text { get; }
    }

    [System.Serializable]
    public class Identifier : Node, IIdentifier
    {
        public virtual string Text { get; set; }
    }
}
