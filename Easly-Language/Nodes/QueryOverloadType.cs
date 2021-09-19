namespace BaseNode
{
    /// <summary>
    /// Represents a query overload type in a type definition.
    /// /Doc/Nodes/QueryOverloadType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class QueryOverloadType : Node
    {
        /// <summary>
        /// Gets or sets the parameters.
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
        /// Gets or sets the list of requirements.
        /// </summary>
        public virtual IBlockList<Assertion> RequireBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of guaranties.
        /// </summary>
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of exception handlers.
        /// </summary>
        public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; } = null!;
    }
}
