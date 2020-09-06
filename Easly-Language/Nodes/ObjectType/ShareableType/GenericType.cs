#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IGenericType : IShareableType
    {
        IIdentifier ClassIdentifier { get; }
        IBlockList<ITypeArgument, TypeArgument> TypeArgumentBlocks { get; }
    }

    [System.Serializable]
    public class GenericType : ShareableType, IGenericType
    {
        public virtual IIdentifier ClassIdentifier { get; set; }
        public virtual IBlockList<ITypeArgument, TypeArgument> TypeArgumentBlocks { get; set; }
    }
}
