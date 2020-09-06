#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IWith : INode
    {
        IBlockList<IRange, Range> RangeBlocks { get; }
        IScope Instructions { get; }
    }

    [System.Serializable]
    public class With : Node, IWith
    {
        public virtual IBlockList<IRange, Range> RangeBlocks { get; set; }
        public virtual IScope Instructions { get; set; }
    }
}
