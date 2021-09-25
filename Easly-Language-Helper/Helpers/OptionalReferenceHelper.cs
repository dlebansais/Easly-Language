namespace BaseNodeHelper
{
    using BaseNode;
    using Easly;

    /// <summary>
    /// Provides methods to manipulate optional references.
    /// </summary>
    public static class OptionalReferenceHelper
    {
        /// <inheritdoc cref="OptionalReferenceHelper{TNode}.CreateEmptyReference"/>
        public static IOptionalReference<TNode> CreateEmptyReference<TNode>()
            where TNode : Node
        {
            return OptionalReferenceHelper<TNode>.CreateEmptyReference();
        }

        /// <inheritdoc cref="OptionalReferenceHelper{TNode}.CreateReference"/>
        public static IOptionalReference<TNode> CreateReference<TNode>(TNode item)
            where TNode : Node
        {
            return OptionalReferenceHelper<TNode>.CreateReference(item);
        }

        /// <inheritdoc cref="OptionalReferenceHelper{TNode}.CreateReferenceCopy"/>
        public static IOptionalReference<TNode> CreateReferenceCopy<TNode>(IOptionalReference<TNode> optional)
            where TNode : Node
        {
            return OptionalReferenceHelper<TNode>.CreateReferenceCopy(optional);
        }
    }
}
