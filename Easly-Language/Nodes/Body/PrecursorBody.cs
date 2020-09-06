namespace BaseNode
{
    using Easly;

    public interface IPrecursorBody : IBody
    {
        IOptionalReference<IObjectType> AncestorType { get; }
    }

    [System.Serializable]
    public class PrecursorBody : Body, IPrecursorBody
    {
        public virtual IOptionalReference<IObjectType> AncestorType { get; set; }
    }
}
