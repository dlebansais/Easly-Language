using Easly;

namespace BaseNode
{
    public interface IDocument
    {
        string Comment { get; set; }
        System.Guid Uuid { get; set; }
    }

    [System.Serializable]
    public class Document : IDocument
    {
        public virtual string Comment { get; set; }
        public virtual System.Guid Uuid { get; set; }
    }
}
