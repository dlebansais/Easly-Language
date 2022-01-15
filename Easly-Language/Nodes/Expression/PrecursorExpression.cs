namespace BaseNode;

using Easly;

/// <summary>
/// Represents the expression from the precursor of a query.
/// /Doc/Nodes/Expression/PrecursorExpression.md explains the semantic.
/// </summary>
[System.Serializable]
public class PrecursorExpression : Expression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrecursorExpression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="ancestorType">The type where to get the precursor from.</param>
    /// <param name="argumentBlocks">The query parameters.</param>
    internal PrecursorExpression(Document documentation, IOptionalReference<ObjectType> ancestorType, IBlockList<Argument> argumentBlocks)
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
