namespace BaseNode;

/// <summary>
/// Represents the scope of a set of instructions.
/// /Doc/Nodes/Scope.md explains the semantic.
/// </summary>
[System.Serializable]
public class Scope : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Scope()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        EntityDeclarationBlocks = default!;
        InstructionBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Scope"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityDeclarationBlocks">The scope variables.</param>
    /// <param name="instructionBlocks">The list of instructions.</param>
    internal Scope(Document documentation, IBlockList<EntityDeclaration> entityDeclarationBlocks, IBlockList<Instruction> instructionBlocks)
        : base(documentation)
    {
        EntityDeclarationBlocks = entityDeclarationBlocks;
        InstructionBlocks = instructionBlocks;
    }

    /// <summary>
    /// Gets or sets the scope variables.
    /// </summary>
    public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; }

    /// <summary>
    /// Gets or sets the list of instructions.
    /// </summary>
    public virtual IBlockList<Instruction> InstructionBlocks { get; set; }
}
