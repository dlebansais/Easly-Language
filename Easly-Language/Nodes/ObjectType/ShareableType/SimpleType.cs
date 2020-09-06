#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface ISimpleType : IShareableType
    {
        IIdentifier ClassIdentifier { get; }
    }

    [System.Serializable]
    public class SimpleType : ShareableType, ISimpleType
    {
        public virtual IIdentifier ClassIdentifier { get; set; }
    }
}
