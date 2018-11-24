namespace BaseNode
{
    public interface IExportChange : INode
    {
        IIdentifier ExportIdentifier { get; }
        IBlockList<IIdentifier, Identifier> IdentifierBlocks { get; }
    }

    [System.Serializable]
    public class ExportChange : Node, IExportChange
    {
        public virtual IIdentifier ExportIdentifier { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> IdentifierBlocks { get; set; }
    }
}
