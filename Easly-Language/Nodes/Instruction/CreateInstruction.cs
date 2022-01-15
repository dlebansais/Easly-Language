namespace BaseNode;

using Easly;

/// <summary>
/// Represents an object creation instruction.
/// /Doc/Nodes/Instruction/CreateInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class CreateInstruction : Instruction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityIdentifier">The created object.</param>
    /// <param name="creationRoutineIdentifier">The routine to call when creating.</param>
    /// <param name="argumentBlocks">The call arguments.</param>
    /// <param name="processor">The target processor.</param>
    internal CreateInstruction(Document documentation, Identifier entityIdentifier, Identifier creationRoutineIdentifier, IBlockList<Argument> argumentBlocks, IOptionalReference<QualifiedName> processor)
        : base(documentation)
    {
        EntityIdentifier = entityIdentifier;
        CreationRoutineIdentifier = creationRoutineIdentifier;
        ArgumentBlocks = argumentBlocks;
        Processor = processor;
    }

    /// <summary>
    /// Gets or sets the created object.
    /// </summary>
    public virtual Identifier EntityIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the routine to call when creating.
    /// </summary>
    public virtual Identifier CreationRoutineIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the call arguments.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }

    /// <summary>
    /// Gets or sets the target processor.
    /// </summary>
    public virtual IOptionalReference<QualifiedName> Processor { get; set; }
}
