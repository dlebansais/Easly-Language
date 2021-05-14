#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    public interface ILibrary : INode
    {
        IName EntityName { get; }
        IOptionalReference<IIdentifier> FromIdentifier { get; }
        IBlockList<IImport, Import> ImportBlocks { get; }
        IBlockList<IIdentifier, Identifier> ClassIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class Library : Node, ILibrary
    {
        public virtual IName EntityName { get; set; }
        public virtual IOptionalReference<IIdentifier> FromIdentifier { get; set; }
        public virtual IBlockList<IImport, Import> ImportBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ClassIdentifierBlocks { get; set; }
    }
}
