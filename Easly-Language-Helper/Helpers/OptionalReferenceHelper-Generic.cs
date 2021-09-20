#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System.Diagnostics;
    using BaseNode;
    using Easly;

    public static class OptionalReferenceHelper<TNode>
        where TNode : Node
    {
        internal static IOptionalReference<TNode> CreateEmptyReference()
        {
            OptionalReference<TNode> Result = new OptionalReference<TNode>();
            Debug.Assert(!Result.IsAssigned, "An empty reference is never assigned");
            Debug.Assert(!Result.HasItem, "An empty reference is empty");

            return Result;
        }

        internal static IOptionalReference<TNode> CreateReference(TNode item)
        {
            OptionalReference<TNode> Result = new OptionalReference<TNode>();
            Result.Item = item;
            Result.Unassign();

            return Result;
        }

        internal static IOptionalReference<TNode> CreateReferenceCopy(IOptionalReference<TNode> optional)
        {
            OptionalReference<TNode> Result = new OptionalReference<TNode>();

            if (optional != null)
            {
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
            }

            return Result;
        }
    }
}
