namespace BaseNode;

/// <summary>
/// Represents a debugging instruction.
/// /Doc/Nodes/Instruction/DebugInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class DebugInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public DebugInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Instructions = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="DebugInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="instructions">Instructions to execute conditionally.</param>
    internal DebugInstruction(Document documentation, Scope instructions)
        : base(documentation)
    {
        Instructions = instructions;
    }

    /// <summary>
    /// Gets or sets instructions to execute conditionally.
    /// </summary>
    public virtual Scope Instructions { get; set; }
}
