using Easly;

namespace BaseNode
{
    public interface IPrecursorBody : IBody
    {
        OptionalReference<IObjectType> AncestorType { get; }
    }

    [System.Serializable]
    public class PrecursorBody : Body, IPrecursorBody
    {
        public virtual OptionalReference<IObjectType> AncestorType { get; set; }
    }
}
