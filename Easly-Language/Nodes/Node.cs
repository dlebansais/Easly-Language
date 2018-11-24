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
