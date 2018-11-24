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
