namespace BaseNode
{
    /// <summary>
    /// Represents an exception handler.
    /// /Doc/Nodes/ExceptionHandler.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ExceptionHandler : Node
    {
        /// <summary>
        /// Gets or sets the identifier of the handled exception.
        /// </summary>
        public virtual Identifier ExceptionIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets instructions to execute.
        /// </summary>
        public virtual Scope Instructions { get; set; } = null!;
    }
}
