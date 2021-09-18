namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents the expression from the precursor of a query.
    /// /Doc/Nodes/Expression/PrecursorExpression.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PrecursorExpression : Expression
    {
        /// <summary>
        /// Gets or sets the type where to get the precursor from.
        /// </summary>
        public virtual IOptionalReference<ObjectType> AncestorType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        public virtual IBlockList<Argument> ArgumentBlocks { get; set; } = null!;
    }
}
