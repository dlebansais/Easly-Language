using Easly;

namespace BaseNode
{
    public interface IImport : INode
    {
        IIdentifier LibraryIdentifier { get; }
        OptionalReference<IIdentifier> FromIdentifier { get; }
        ImportType Type { get; }
        IBlockList<IRename, Rename> RenameBlocks { get; }
    }

    [System.Serializable]
    public class Import : Node, IImport
    {
        public virtual IIdentifier LibraryIdentifier { get; set; }
        public virtual OptionalReference<IIdentifier> FromIdentifier { get; set; }
        public virtual ImportType Type { get; set; }
        public virtual IBlockList<IRename, Rename> RenameBlocks { get; set; }
    }
}
