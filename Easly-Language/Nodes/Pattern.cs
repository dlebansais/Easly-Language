#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IPattern : INode
    {
        string Text { get; }
    }

    [System.Serializable]
    public class Pattern : Node, IPattern
    {
        public virtual string Text { get; set; }
    }
}
