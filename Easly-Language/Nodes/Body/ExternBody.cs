namespace BaseNode;

/// <summary>
/// Represents an extern body.
/// /Doc/Nodes/Body/ExternBody.md explains the semantic.
/// </summary>
[System.Serializable]
public class ExternBody : Body
{
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
