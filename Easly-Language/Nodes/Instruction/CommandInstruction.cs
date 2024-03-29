namespace BaseNode;

/// <summary>
/// Represents a command instruction.
/// /Doc/Nodes/Instruction/CommandInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class CommandInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public CommandInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        Command = default!;
        ArgumentBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="command">The feature to call.</param>
    /// <param name="argumentBlocks">The call arguments.</param>
    internal CommandInstruction(Document documentation, QualifiedName command, IBlockList<Argument> argumentBlocks)
        : base(documentation)
    {
        Command = command;
        ArgumentBlocks = argumentBlocks;
    }

    /// <summary>
    /// Gets or sets the feature to call.
    /// </summary>
    public virtual QualifiedName Command { get; set; }

    /// <summary>
    /// Gets or sets the call arguments.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }
}
