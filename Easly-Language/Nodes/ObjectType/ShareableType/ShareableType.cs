namespace BaseNode
{
    public interface IShareableType : IObjectType
    {
        SharingType Sharing { get; }
    }

    public abstract class ShareableType : ObjectType, IShareableType
    {
        public virtual SharingType Sharing { get; set; }
    }
}
