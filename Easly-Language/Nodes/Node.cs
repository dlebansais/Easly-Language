#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface INode
    {
        IDocument Documentation { get; }
    }

    [System.Serializable]
    public abstract class Node : INode
    {
        public virtual IDocument Documentation { get; set; }
    }
}
