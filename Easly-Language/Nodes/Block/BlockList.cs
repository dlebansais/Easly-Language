#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
        IList IBlockList.NodeBlockList { get { return (IList)NodeBlockList; } }
    }
}
