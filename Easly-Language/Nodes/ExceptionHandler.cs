namespace BaseNode;

/// <summary>
/// Represents an exception handler.
/// /Doc/Nodes/ExceptionHandler.md explains the semantic.
/// </summary>
[System.Serializable]
public class ExceptionHandler : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandler"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exceptionIdentifier">The identifier of the handled exception.</param>
    /// <param name="instructions">The instructions to execute.</param>
    internal ExceptionHandler(Document documentation, Identifier exceptionIdentifier, Scope instructions)
        : base(documentation)
    {
        ExceptionIdentifier = exceptionIdentifier;
        Instructions = instructions;
    }

    /// <summary>
    /// Gets or sets the identifier of the handled exception.
    /// </summary>
    public virtual Identifier ExceptionIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the instructions to execute.
    /// </summary>
    public virtual Scope Instructions { get; set; }
}
