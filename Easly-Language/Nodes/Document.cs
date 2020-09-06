#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

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
