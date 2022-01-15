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
        /// Initializes a new instance of the <see cref="QueryOverloadType"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="parameterBlocks">The parameters.</param>
        /// <param name="parameterEnd">Whether the query accepts extra parameters.</param>
        /// <param name="resultBlocks">The list of results.</param>
        /// <param name="requireBlocks">The list of requirements.</param>
        /// <param name="ensureBlocks">The list of guarantees.</param>
        /// <param name="exceptionIdentifierBlocks">The list of exception handlers.</param>
        internal QueryOverloadType(Document documentation, IBlockList<EntityDeclaration> parameterBlocks, ParameterEndStatus parameterEnd, IBlockList<EntityDeclaration> resultBlocks, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
            : base(documentation)
        {
            ParameterBlocks = parameterBlocks;
            ParameterEnd = parameterEnd;
            ResultBlocks = resultBlocks;
            RequireBlocks = requireBlocks;
            EnsureBlocks = ensureBlocks;
            ExceptionIdentifierBlocks = exceptionIdentifierBlocks;
        }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; }

        /// <summary>
        /// Gets or sets whether the query accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; }

        /// <summary>
        /// Gets or sets the list of results.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> ResultBlocks { get; set; }

        /// <summary>
        /// Gets or sets the list of requirements.
        /// </summary>
        public virtual IBlockList<Assertion> RequireBlocks { get; set; }

        /// <summary>
        /// Gets or sets the list of guarantees.
        /// </summary>
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; }

        /// <summary>
        /// Gets or sets the list of exception handlers.
        /// </summary>
        public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; }
    }
}
