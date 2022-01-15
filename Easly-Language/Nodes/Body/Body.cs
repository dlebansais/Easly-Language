namespace BaseNode;

/// <summary>
/// Represents any feature body.
/// </summary>
public abstract class Body : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Body"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="requireBlocks">The list of contract requirements.</param>
    /// <param name="ensureBlocks">The list of contract guarantees.</param>
    /// <param name="exceptionIdentifierBlocks">The list of exceptions.</param>
    internal Body(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
        : base(documentation)
    {
        RequireBlocks = requireBlocks;
        EnsureBlocks = ensureBlocks;
        ExceptionIdentifierBlocks = exceptionIdentifierBlocks;
    }

    /// <summary>
    /// Gets or sets the list of contract requirements.
    /// </summary>
    public virtual IBlockList<Assertion> RequireBlocks { get; set; }

    /// <summary>
    /// Gets or sets the list of contract guarantees.
    /// </summary>
    public virtual IBlockList<Assertion> EnsureBlocks { get; set; }

    /// <summary>
    /// Gets or sets the list of exceptions.
    /// </summary>
    public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; }
}
