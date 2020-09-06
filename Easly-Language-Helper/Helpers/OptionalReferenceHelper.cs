namespace BaseNodeHelper
{
    using System.Diagnostics;
    using BaseNode;
    using Easly;

    public static class OptionalReferenceHelper
    {
        public static IOptionalReference<TNodeInterface> CreateEmptyReference<TNodeInterface>()
            where TNodeInterface : class, INode
        {
            return OptionalReferenceHelper<TNodeInterface>.CreateEmptyReference();
        }

        public static IOptionalReference<TNodeInterface> CreateReference<TNodeInterface>(TNodeInterface item)
            where TNodeInterface : class, INode
        {
            return OptionalReferenceHelper<TNodeInterface>.CreateReference(item);
        }

        public static IOptionalReference<TNodeInterface> CreateReferenceCopy<TNodeInterface>(IOptionalReference<TNodeInterface> optional)
            where TNodeInterface : class, INode
        {
            return OptionalReferenceHelper<TNodeInterface>.CreateReferenceCopy(optional);
        }
    }

    public static class OptionalReferenceHelper<TNodeInterface>
        where TNodeInterface : class, INode
    {
        internal static IOptionalReference<TNodeInterface> CreateEmptyReference()
        {
            OptionalReference<TNodeInterface> Result = new OptionalReference<TNodeInterface>();
            Debug.Assert(!Result.IsAssigned, "An empty reference is never assigned");
            Debug.Assert(!Result.HasItem, "An empty reference is empty");

            return Result;
        }

        internal static IOptionalReference<TNodeInterface> CreateReference(TNodeInterface item)
        {
            OptionalReference<TNodeInterface> Result = new OptionalReference<TNodeInterface>();
            Result.Item = item;
            Result.Unassign();

            return Result;
        }

        internal static IOptionalReference<TNodeInterface> CreateReferenceCopy(IOptionalReference<TNodeInterface> optional)
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
