namespace BaseNode;

using Easly;

/// <summary>
/// Represents a query overload in a feature.
/// /Doc/Nodes/QueryOverload.md explains the semantic.
/// </summary>
[System.Serializable]
public class QueryOverload : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public QueryOverload()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ParameterBlocks = default!;
        ParameterEnd = default!;
        ResultBlocks = default!;
        ModifiedQueryBlocks = default!;
        Variant = default!;
        QueryBody = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryOverload"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parameterBlocks">The list of parameters.</param>
    /// <param name="parameterEnd">Whether the query accepts extra parameters.</param>
    /// <param name="resultBlocks">The list of results.</param>
    /// <param name="modifiedQueryBlocks">The list of other features this query modifies.</param>
    /// <param name="variant">The query variant.</param>
    /// <param name="queryBody">The query body.</param>
    internal QueryOverload(Document documentation, IBlockList<EntityDeclaration> parameterBlocks, ParameterEndStatus parameterEnd, IBlockList<EntityDeclaration> resultBlocks, IBlockList<Identifier> modifiedQueryBlocks, IOptionalReference<Expression> variant, Body queryBody)
        : base(documentation)
    {
        ParameterBlocks = parameterBlocks;
        ParameterEnd = parameterEnd;
        ResultBlocks = resultBlocks;
        ModifiedQueryBlocks = modifiedQueryBlocks;
        Variant = variant;
        QueryBody = queryBody;
    }

    /// <summary>
    /// Gets or sets the list of parameters.
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
    /// Gets or sets the list of other features this query modifies.
    /// </summary>
    public virtual IBlockList<Identifier> ModifiedQueryBlocks { get; set; }

    /// <summary>
    /// Gets or sets the query variant.
    /// </summary>
    public virtual IOptionalReference<Expression> Variant { get; set; }

    /// <summary>
    /// Gets or sets the query body.
    /// </summary>
    public virtual Body QueryBody { get; set; }
}
