#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IBlockList
    {
        Document Documentation { get; }
        IList NodeBlockList { get; }
    }

    [System.Serializable]
    public class BlockList<TNode> : IBlockList
        where TNode : Node
    {
        public virtual Document Documentation { get; set; }
        Document IBlockList.Documentation { get { return Documentation; } }
        public virtual IList<Block<TNode>> NodeBlockList { get; set; }
        IList IBlockList.NodeBlockList { get { return (IList)NodeBlockList; } }
    }
}
