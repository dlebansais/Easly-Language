namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a query overload in a feature.
    /// /Doc/Nodes/QueryOverload.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class QueryOverload : Node
    {
        /// <summary>
        /// Gets or sets the list of parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the query accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; } = ParameterEndStatus.Closed;

        /// <summary>
        /// Gets or sets the list of results.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> ResultBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of other features this query modifies.
        /// </summary>
        public virtual IBlockList<Identifier> ModifiedQueryBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the query variant.
        /// </summary>
        public virtual IOptionalReference<Expression> Variant { get; set; } = null!;

        /// <summary>
        /// Gets or sets the query body.
        /// </summary>
        public virtual Body QueryBody { get; set; } = null!;
    }
}
