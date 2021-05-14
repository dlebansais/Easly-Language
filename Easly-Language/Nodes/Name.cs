#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
