namespace BaseNode
{
    /// <summary>
    /// Represents a query expression using an indexer.
    /// /Doc/Nodes/Expression/IndexQueryExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class IndexQueryExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexQueryExpression"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="indexedExpression">The indexed expression.</param>
        /// <param name="argumentBlocks">Query parameters.</param>
        internal IndexQueryExpression(Document documentation, Expression indexedExpression, IBlockList<Argument> argumentBlocks)
            : base(documentation)
        {
            IndexedExpression = indexedExpression;
            ArgumentBlocks = argumentBlocks;
        }

        /// <summary>
        /// Gets or sets the indexed expression.
        /// </summary>
        public virtual Expression IndexedExpression { get; set; }

        /// <summary>
        /// Gets or sets query parameters.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; }
    }
}
