namespace BaseNode;

using Easly;

/// <summary>
/// Represents the expression from the precursor of an indexer.
/// /Doc/Nodes/Expression/PrecursorIndexExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class PrecursorIndexExpression : Expression
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public PrecursorIndexExpression()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        AncestorType = default!;
        ArgumentBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="PrecursorIndexExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="ancestorType">The type where to get the precursor from.</param>
    /// <param name="argumentBlocks">The query parameters.</param>
    internal PrecursorIndexExpression(Document documentation, IOptionalReference<ObjectType> ancestorType, IBlockList<Argument> argumentBlocks)
        : base(documentation)
    {
        AncestorType = ancestorType;
        ArgumentBlocks = argumentBlocks;
    }

    /// <summary>
    /// Gets or sets the type where to get the precursor from.
    /// </summary>
    public virtual IOptionalReference<ObjectType> AncestorType { get; set; }

    /// <summary>
    /// Gets or sets the query parameters.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }
}
