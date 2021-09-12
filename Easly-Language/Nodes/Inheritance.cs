#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    [System.Serializable]
    public class Inheritance : Node
    {
        public virtual ObjectType ParentType { get; set; }
        public virtual ConformanceType Conformance { get; set; }
        public virtual BlockList<Rename> RenameBlocks { get; set; }
        public virtual bool ForgetIndexer { get; set; }
        public virtual BlockList<Identifier> ForgetBlocks { get; set; }
        public virtual bool KeepIndexer { get; set; }
        public virtual BlockList<Identifier> KeepBlocks { get; set; }
        public virtual bool DiscontinueIndexer { get; set; }
        public virtual BlockList<Identifier> DiscontinueBlocks { get; set; }
        public virtual BlockList<ExportChange> ExportChangeBlocks { get; set; }
    }
}
