using Easly;

namespace BaseNode
{
    public interface IDocument
    {
        string Comment { get; }
        System.Guid Uuid { get; }
    }

    [System.Serializable]
    public class Document : IDocument
    {
        public virtual string Comment { get; set; }
        public virtual System.Guid Uuid { get; set; }
    }
}
