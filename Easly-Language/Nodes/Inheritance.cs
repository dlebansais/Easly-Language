namespace BaseNode
{
    public interface IInheritance : INode
    {
        IObjectType ParentType { get; }
        ConformanceType Conformance { get; }
        IBlockList<IRename, Rename> RenameBlocks { get; }
        bool ForgetIndexer { get; }
        IBlockList<IIdentifier, Identifier> ForgetBlocks { get; }
        bool KeepIndexer { get; }
        IBlockList<IIdentifier, Identifier> KeepBlocks { get; }
        bool DiscontinueIndexer { get; }
        IBlockList<IIdentifier, Identifier> DiscontinueBlocks { get; }
        IBlockList<IExportChange, ExportChange> ExportChangeBlocks { get; }
    }

    [System.Serializable]
    public class Inheritance : Node, IInheritance
    {
        public virtual IObjectType ParentType { get; set; }
        public virtual ConformanceType Conformance { get; set; }
        public virtual IBlockList<IRename, Rename> RenameBlocks { get; set; }
        public virtual bool ForgetIndexer { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ForgetBlocks { get; set; }
        public virtual bool KeepIndexer { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> KeepBlocks { get; set; }
        public virtual bool DiscontinueIndexer { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> DiscontinueBlocks { get; set; }
        public virtual IBlockList<IExportChange, ExportChange> ExportChangeBlocks { get; set; }
    }
}