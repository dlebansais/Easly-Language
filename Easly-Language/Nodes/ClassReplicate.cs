#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IClassReplicate : INode
    {
        IName ReplicateName { get; }
        IBlockList<IPattern, Pattern> PatternBlocks { get; }
    }

    [System.Serializable]
    public class ClassReplicate : Node, IClassReplicate
    {
        public virtual IName ReplicateName { get; set; }
        public virtual IBlockList<IPattern, Pattern> PatternBlocks { get; set; }
    }
}
