namespace BaseNode
{
    /// <summary>
    /// Represents any feature body.
    /// </summary>
    public abstract class Body : Node
    {
        /// <summary>
        /// Gets or sets the list of contract requirements.
        /// </summary>
        public virtual IBlockList<Assertion> RequireBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of contract guaranties.
        /// </summary>
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of exceptions.
        /// </summary>
        public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; } = null!;
    }
}
