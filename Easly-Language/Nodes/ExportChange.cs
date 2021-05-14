#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
