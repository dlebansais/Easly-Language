namespace BaseNode
{
    /// <summary>
    /// Represents a query expression.
    /// /Doc/Nodes/Expression/QueryExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class QueryExpression : Expression
    {
        /// <summary>
        /// Gets or sets the path to the queried feature.
        /// </summary>
        public virtual QualifiedName Query { get; set; } = null!;

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;
    }
}
