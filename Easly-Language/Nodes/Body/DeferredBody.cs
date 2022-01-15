namespace BaseNode
{
    /// <summary>
    /// Represents an deferred body.
    /// /Doc/Nodes/Body/DeferredBody.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class DeferredBody : Body
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredBody"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="requireBlocks">The list of contract requirements.</param>
        /// <param name="ensureBlocks">The list of contract guarantees.</param>
        /// <param name="exceptionIdentifierBlocks">The list of exceptions.</param>
        internal DeferredBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
            : base(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks)
        {
        }
    }
}
