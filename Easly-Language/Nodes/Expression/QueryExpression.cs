namespace BaseNode;

/// <summary>
/// Represents a query expression.
/// /Doc/Nodes/Expression/QueryExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class QueryExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public QueryExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Query = default!;
        ArgumentBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="QueryExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="query">The path to the queried feature.</param>
    /// <param name="argumentBlocks">The query parameters.</param>
    internal QueryExpression(Document documentation, QualifiedName query, IBlockList<Argument> argumentBlocks)
        : base(documentation)
    {
        Query = query;
        ArgumentBlocks = argumentBlocks;
    }

    /// <summary>
    /// Gets or sets the path to the queried feature.
    /// </summary>
    public virtual QualifiedName Query { get; set; }

    /// <summary>
    /// Gets or sets the query parameters.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }
}
