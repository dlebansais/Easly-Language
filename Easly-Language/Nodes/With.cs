#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
