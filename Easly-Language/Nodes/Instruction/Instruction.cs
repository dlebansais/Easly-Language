namespace BaseNode;

/// <summary>
/// Represents any instruction.
/// </summary>
public abstract class Instruction : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Instruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    internal Instruction(Document documentation)
        : base(documentation)
    {
    }
}
