namespace BaseNode;

/// <summary>
/// Represents the scope of a set of instructions.
/// /Doc/Nodes/Scope.md explains the semantic.
/// </summary>
[System.Serializable]
public class Scope : Node
{
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
