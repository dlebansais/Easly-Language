namespace BaseNode
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of blocks of nodes.
    /// </summary>
    public interface IBlockList
    {
        /// <summary>
        /// Gets the documentation.
        /// </summary>
        Document Documentation { get; }

        /// <summary>
        /// Gets the list of blocks.
        /// </summary>
        IList NodeBlockList { get; }
    }

    /// <summary>
    /// Represents a list of blocks of nodes.
    /// </summary>
    /// <typeparam name="TNode">Type of the node.</typeparam>
    public interface IBlockList<TNode>
        where TNode : Node
    {
        /// <summary>
        /// Gets the documentation.
        /// </summary>
        Document Documentation { get; }

        /// <summary>
        /// Gets the list of blocks.
        /// </summary>
        IList<IBlock<TNode>> NodeBlockList { get; }
    }

    /// <inheritdoc/>
    [System.Serializable]
    public class BlockList<TNode> : IBlockList<TNode>, IBlockList
        where TNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockList{TNode}"/> class.
        /// </summary>
        /// <param name="documentation">The block list documentation.</param>
        /// <param name="nodeBlockList">The list of blocks.</param>
        internal BlockList(Document documentation, IList<IBlock<TNode>> nodeBlockList)
        {
            Documentation = documentation;
            NodeBlockList = nodeBlockList;
        }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public virtual Document Documentation { get; set; }

        /// <inheritdoc/>
        Document IBlockList.Documentation { get { return Documentation; } }

        /// <summary>
        /// Gets or sets the list of blocks.
        /// </summary>
        public virtual IList<IBlock<TNode>> NodeBlockList { get; set; }

        /// <inheritdoc/>
        IList IBlockList.NodeBlockList { get { return (IList)NodeBlockList; } }
    }
}
