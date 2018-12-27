using System.Collections;
using System.Collections.Generic;

namespace BaseNode
{
    public interface IBlockList
    {
        IDocument Documentation { get; }
        IList NodeBlockList { get; }
    }

    public interface IBlockList<IN, N>
        where IN : class, INode
        where N : Node, IN
    {
        IDocument Documentation { get; }
        IList<IBlock<IN, N>> NodeBlockList { get; }
    }

    [System.Serializable]
    public class BlockList<IN, N> : IBlockList<IN, N>, IBlockList
        where IN : class, INode
        where N : Node, IN
    {
        public virtual IDocument Documentation { get; set; }
        IDocument IBlockList.Documentation { get { return Documentation; } }
        public virtual IList<IBlock<IN, N>> NodeBlockList { get; set; }
        IList IBlockList.NodeBlockList { get { return NodeBlockList as IList; } }
    }
}
