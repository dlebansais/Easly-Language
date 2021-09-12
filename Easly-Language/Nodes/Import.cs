#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class Import : Node
    {
        public virtual Identifier LibraryIdentifier { get; set; }
        public virtual OptionalReference<Identifier> FromIdentifier { get; set; }
        public virtual ImportType Type { get; set; }
        public virtual BlockList<Rename> RenameBlocks { get; set; }
    }
}
