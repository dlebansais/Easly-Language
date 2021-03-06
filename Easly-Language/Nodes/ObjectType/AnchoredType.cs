#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
