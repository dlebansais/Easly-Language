#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface IImport : INode
    {
        IIdentifier LibraryIdentifier { get; }
        IOptionalReference<IIdentifier> FromIdentifier { get; }
        ImportType Type { get; }
        IBlockList<IRename, Rename> RenameBlocks { get; }
    }

    [System.Serializable]
    public class Import : Node, IImport
    {
        public virtual IIdentifier LibraryIdentifier { get; set; }
        public virtual IOptionalReference<IIdentifier> FromIdentifier { get; set; }
        public virtual ImportType Type { get; set; }
        public virtual IBlockList<IRename, Rename> RenameBlocks { get; set; }
    }
}
