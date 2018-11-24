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
