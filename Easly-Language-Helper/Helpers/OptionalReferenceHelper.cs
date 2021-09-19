#pragma warning disable SA1600 // Elements should be documented

namespace BaseNodeHelper
{
    using System.Diagnostics;
    using BaseNode;
    using Easly;

    public static class OptionalReferenceHelper
    {
        public static IOptionalReference<TNode> CreateEmptyReference<TNode>()
            where TNode : Node
        {
            return OptionalReferenceHelper<TNode>.CreateEmptyReference();
        }

        public static IOptionalReference<TNode> CreateReference<TNode>(TNode item)
            where TNode : Node
        {
            return OptionalReferenceHelper<TNode>.CreateReference(item);
        }

        public static IOptionalReference<TNode> CreateReferenceCopy<TNode>(IOptionalReference<TNode> optional)
            where TNode : Node
        {
            return OptionalReferenceHelper<TNode>.CreateReferenceCopy(optional);
        }
    }
}
