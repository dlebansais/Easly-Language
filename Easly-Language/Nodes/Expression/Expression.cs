namespace BaseNode;

/// <summary>
/// Represents any expression.
/// </summary>
public abstract class Expression : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Expression"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    internal Expression(Document documentation)
        : base(documentation)
    {
    }
}
