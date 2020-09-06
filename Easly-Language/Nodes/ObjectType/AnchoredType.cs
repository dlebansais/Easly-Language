#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IAnchoredType : IObjectType
    {
        IQualifiedName AnchoredName { get; }
        AnchorKinds AnchorKind { get; }
    }

    [System.Serializable]
    public class AnchoredType : ObjectType, IAnchoredType
    {
        public virtual IQualifiedName AnchoredName { get; set; }
        public virtual AnchorKinds AnchorKind { get; set; }
    }
}
