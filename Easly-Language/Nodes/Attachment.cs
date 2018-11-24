namespace BaseNode
{
    public interface IAttachment : INode
    {
        IBlockList<IObjectType, ObjectType> AttachTypeBlocks { get; }
        IScope Instructions { get; }
    }

    [System.Serializable]
    public class Attachment : Node, IAttachment
    {
        public virtual IBlockList<IObjectType, ObjectType> AttachTypeBlocks { get; set; }
        public virtual IScope Instructions { get; set; }
    }
}
