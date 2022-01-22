namespace BaseNode;

/// <summary>
/// Represents a query expression using an indexer.
/// /Doc/Nodes/Expression/IndexQueryExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class IndexQueryExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public IndexQueryExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        IndexedExpression = default!;
        ArgumentBlocks = default!;
    }
#endif
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
