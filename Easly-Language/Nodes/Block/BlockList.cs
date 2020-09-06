namespace BaseNode
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IBlockList
    {
        IDocument Documentation { get; }
        IList NodeBlockList { get; }
    }

    public interface IBlockList<TNodeInterface, TNode>
        where TNodeInterface : class, INode
        where TNode : Node, TNodeInterface
    {
        IDocument Documentation { get; }
        IList<IBlock<TNodeInterface, TNode>> NodeBlockList { get; }
    }

    [System.Serializable]
    public class BlockList<TNodeInterface, TNode> : IBlockList<TNodeInterface, TNode>, IBlockList
        where TNodeInterface : class, INode
        where TNode : Node, TNodeInterface
    {
        public virtual IDocument Documentation { get; set; }
        IDocument IBlockList.Documentation { get { return Documentation; } }
        public virtual IList<IBlock<TNodeInterface, TNode>> NodeBlockList { get; set; }
        IList IBlockList.NodeBlockList { get { return NodeBlockList as IList; } }
    }
}
