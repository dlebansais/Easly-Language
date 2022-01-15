namespace BaseNode;

using Easly;

/// <summary>
/// Represents a precursor body.
/// /Doc/Nodes/Body/PrecursorBody.md explains the semantic.
/// </summary>
[System.Serializable]
public class PrecursorBody : Body
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrecursorBody"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="requireBlocks">The list of contract requirements.</param>
    /// <param name="ensureBlocks">The list of contract guarantees.</param>
    /// <param name="exceptionIdentifierBlocks">The list of exceptions.</param>
    /// <param name="ancestorType">The ancestor type in case of multiple ancestors.</param>
    internal PrecursorBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IOptionalReference<ObjectType> ancestorType)
        : base(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks)
    {
        AncestorType = ancestorType;
    }

    /// <summary>
    /// Gets or sets the ancestor type in case of multiple ancestors.
    /// </summary>
    public virtual IOptionalReference<ObjectType> AncestorType { get; set; }
}
