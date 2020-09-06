#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IFeature : INode
    {
        IIdentifier ExportIdentifier { get; }
        ExportStatus Export { get; }
    }

    public abstract class Feature : Node, IFeature
    {
        public virtual IIdentifier ExportIdentifier { get; set; }
        public virtual ExportStatus Export { get; set; }
    }
}
