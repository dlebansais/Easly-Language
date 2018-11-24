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
