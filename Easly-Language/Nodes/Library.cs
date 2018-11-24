using Easly;

namespace BaseNode
{
    public interface ILibrary : INode
    {
        IName EntityName { get; }
        OptionalReference<IIdentifier> FromIdentifier { get; }
        IBlockList<IImport, Import> ImportBlocks { get; }
        IBlockList<IIdentifier, Identifier> ClassIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class Library : Node, ILibrary
    {
        public virtual IName EntityName { get; set; }
        public virtual OptionalReference<IIdentifier> FromIdentifier { get; set; }
        public virtual IBlockList<IImport, Import> ImportBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ClassIdentifierBlocks { get; set; }
    }
}