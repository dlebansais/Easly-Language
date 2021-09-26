namespace BaseNodeHelper
{
    using System.Diagnostics;
    using BaseNode;
    using Easly;

    /// <summary>
    /// Provides methods to manipulate optional nodes of a given type in a program tree.
    /// </summary>
    /// <typeparam name="TNode">The node type.</typeparam>
    public static class OptionalReferenceHelper<TNode>
        where TNode : Node
    {
        /// <summary>
        /// Creates a new instance of a <see cref="IOptionalReference{TNode}"/> with no child node.
        /// </summary>
        /// <returns>The created instance.</returns>
        internal static IOptionalReference<TNode> CreateEmptyReference()
        {
            OptionalReference<TNode> Result = new OptionalReference<TNode>();
            Debug.Assert(!Result.IsAssigned, "An empty reference is never assigned");
            Debug.Assert(!Result.HasItem, "An empty reference is empty");

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IOptionalReference{TNode}"/> with the provided child node.
        /// </summary>
        /// <param name="childNode">The child node.</param>
        /// <returns>The created instance.</returns>
        internal static IOptionalReference<TNode> CreateReference(TNode childNode)
        {
            OptionalReference<TNode> Result = new OptionalReference<TNode>();
            Result.Item = childNode;
            Result.Unassign();

            return Result;
        }

        /// <summary>
        /// Creates a new instance of a <see cref="IOptionalReference{TNode}"/> as a copy of the provided reference.
        /// </summary>
        /// <param name="optional">The optional reference to copy.</param>
        /// <returns>The created instance.</returns>
        internal static IOptionalReference<TNode> CreateReferenceCopy(IOptionalReference<TNode> optional)
        {
            OptionalReference<TNode> Result = new OptionalReference<TNode>();

            if (optional.HasItem)
            {
                Debug.Assert(optional.Item != null, $"If {nameof(OptionalReference<TNode>.HasItem)} is true, {nameof(OptionalReference<TNode>.Item)} is never null");

                if (optional.Item != null)
                {
                    TNode ClonedItem = (TNode)NodeHelper.DeepCloneNode(optional.Item, cloneCommentGuid: false);
                    Debug.Assert(ClonedItem != null, "A clone reference is never null");

                    if (ClonedItem != null)
                        Result.Item = ClonedItem;
                }
            }

            if (optional.IsAssigned)
                Result.Assign();

            return Result;
        }
    }
}
