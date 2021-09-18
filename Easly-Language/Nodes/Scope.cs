namespace BaseNode
{
    /// <summary>
    /// Represents the scope of a set of instructions.
    /// /Doc/Nodes/Scope.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Scope : Node
    {
        /// <summary>
        /// Gets or sets the scope variables.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of instructions.
        /// </summary>
        public virtual IBlockList<Instruction> InstructionBlocks { get; set; } = null!;
    }
}
