namespace BaseNodeHelper
{
    using BaseNode;
    using Contracts;
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
            Contract.RequireNotNull(item, out TNode Item);

            return OptionalReferenceHelper<TNode>.CreateReference(Item);
        }

        /// <inheritdoc cref="OptionalReferenceHelper{TNode}.CreateReferenceCopy"/>
        public static IOptionalReference<TNode> CreateReferenceCopy<TNode>(IOptionalReference<TNode> optional)
            where TNode : Node
        {
            Contract.RequireNotNull(optional, out IOptionalReference<TNode> Optional);

            return OptionalReferenceHelper<TNode>.CreateReferenceCopy(Optional);
        }
    }
}
