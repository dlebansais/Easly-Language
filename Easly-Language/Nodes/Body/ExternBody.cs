namespace BaseNode;

/// <summary>
/// Represents an extern body.
/// /Doc/Nodes/Body/ExternBody.md explains the semantic.
/// </summary>
[System.Serializable]
public class ExternBody : Body
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ExternBody()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!, default!, default!, default!)
    {
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ExternBody"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="requireBlocks">The list of contract requirements.</param>
    /// <param name="ensureBlocks">The list of contract guarantees.</param>
    /// <param name="exceptionIdentifierBlocks">The list of exceptions.</param>
    internal ExternBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
        : base(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks)
    {
    }
}
