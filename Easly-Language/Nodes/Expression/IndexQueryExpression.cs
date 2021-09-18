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
        /// Gets or sets the indexed expression.
        /// </summary>
        public virtual Expression IndexedExpression { get; set; } = null!;

        /// <summary>
        /// Gets or sets query parameters.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;
    }
}
