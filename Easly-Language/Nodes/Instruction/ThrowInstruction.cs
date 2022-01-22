namespace BaseNode;

/// <summary>
/// Represents the instruction throwing an exception.
/// /Doc/Nodes/Instruction/ThrowInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class ThrowInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ThrowInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ExceptionType = default!;
        CreationRoutine = default!;
        ArgumentBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ThrowInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="exceptionType">The type of the exception.</param>
    /// <param name="creationRoutine">The creation routine to use.</param>
    /// <param name="argumentBlocks">The call arguments.</param>
    internal ThrowInstruction(Document documentation, ObjectType exceptionType, Identifier creationRoutine, IBlockList<Argument> argumentBlocks)
        : base(documentation)
    {
        ExceptionType = exceptionType;
        CreationRoutine = creationRoutine;
        ArgumentBlocks = argumentBlocks;
    }

    /// <summary>
    /// Gets or sets the type of the exception.
    /// </summary>
    public virtual ObjectType ExceptionType { get; set; }

    /// <summary>
    /// Gets or sets the creation routine to use.
    /// </summary>
    public virtual Identifier CreationRoutine { get; set; }

    /// <summary>
    /// Gets or sets the call arguments.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }
}
