namespace BaseNode
{
    public interface IExport : INode
    {
        IName EntityName { get; }
        IBlockList<IIdentifier, Identifier> ClassIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class Export : Node, IExport
    {
        public virtual IName EntityName { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ClassIdentifierBlocks { get; set; }
    }
}
