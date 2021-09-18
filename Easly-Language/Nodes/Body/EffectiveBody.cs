namespace BaseNode
{
    /// <summary>
    /// Represents an effective body.
    /// /Doc/Nodes/Body/EffectiveBody.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class EffectiveBody : Body
    {
        /// <summary>
        /// Gets or sets the body local variables.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the body instructions.
        /// </summary>
        public virtual IBlockList<Instruction> BodyInstructionBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the body exception handlers.
        /// </summary>
        public virtual IBlockList<ExceptionHandler> ExceptionHandlerBlocks { get; set; } = null!;
    }
}
