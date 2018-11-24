using System.Collections.Generic;

namespace BaseNode
{
    public interface IBlockList
    {
        IDocument Documentation { get; }
    }

    public interface IBlockList<IN, N> : IBlockList
        where IN : class, INode
        where N : Node, IN
    {
        IList<IBlock<IN, N>> NodeBlockList { get; }
    }

    [System.Serializable]
    public class BlockList<IN, N> : IBlockList<IN, N>
        where IN : class, INode
        where N : Node, IN
    {
        public virtual IDocument Documentation { get; set; }
        public virtual IList<IBlock<IN, N>> NodeBlockList { get; set; }
    }
}
