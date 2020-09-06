namespace BaseNodeHelper
{
    using System.Diagnostics;
    using BaseNode;
    using Easly;

    public static class OptionalReferenceHelper<TNodeInterface>
        where TNodeInterface : class, INode
    {
        public static IOptionalReference<TNodeInterface> CreateEmptyReference()
        {
            OptionalReference<TNodeInterface> Result = new OptionalReference<TNodeInterface>();
            Debug.Assert(!Result.IsAssigned, "An empty reference is never assigned");
            Debug.Assert(!Result.HasItem, "An empty reference is empty");

            return Result;
        }

        public static IOptionalReference<TNodeInterface> CreateReference(TNodeInterface item)
        {
            OptionalReference<TNodeInterface> Result = new OptionalReference<TNodeInterface>();
            Result.Item = item;
            Result.Unassign();

            return Result;
        }

        public static IOptionalReference<TNodeInterface> CreateReferenceCopy(IOptionalReference<TNodeInterface> optional)
        {
            OptionalReference<TNodeInterface> Result = new OptionalReference<TNodeInterface>();

            if (optional != null)
            {
                if (optional.HasItem)
                {
                    Debug.Assert(optional.Item != null, $"If {nameof(IOptionalReference<TNodeInterface>.HasItem)} is true, {nameof(IOptionalReference<TNodeInterface>.Item)} is never null");

                    TNodeInterface ClonedItem = NodeHelper.DeepCloneNode(optional.Item, cloneCommentGuid: false) as TNodeInterface;
                    Debug.Assert(ClonedItem != null, "A clone reference is never null");

                    Result.Item = ClonedItem;
                }

                if (optional.IsAssigned)
                    Result.Assign();
            }

            return Result;
        }
    }
}
