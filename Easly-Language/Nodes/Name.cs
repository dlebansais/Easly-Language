#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IName : INode
    {
        string Text { get; }
    }

    [System.Serializable]
    public class Name : Node, IName
    {
        public virtual string Text { get; set; }
    }
}
