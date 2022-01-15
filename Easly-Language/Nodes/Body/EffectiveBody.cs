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
        /// Initializes a new instance of the <see cref="EffectiveBody"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="requireBlocks">The list of contract requirements.</param>
        /// <param name="ensureBlocks">The list of contract guarantees.</param>
        /// <param name="exceptionIdentifierBlocks">The list of exceptions.</param>
        /// <param name="entityDeclarationBlocks">The body local variables.</param>
        /// <param name="bodyInstructionBlocks">The body instructions.</param>
        /// <param name="exceptionHandlerBlocks">The body exception handlers.</param>
        internal EffectiveBody(Document documentation, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks, IBlockList<EntityDeclaration> entityDeclarationBlocks, IBlockList<Instruction> bodyInstructionBlocks, IBlockList<ExceptionHandler> exceptionHandlerBlocks)
            : base(documentation, requireBlocks, ensureBlocks, exceptionIdentifierBlocks)
        {
            EntityDeclarationBlocks = entityDeclarationBlocks;
            BodyInstructionBlocks = bodyInstructionBlocks;
            ExceptionHandlerBlocks = exceptionHandlerBlocks;
        }

        /// <summary>
        /// Gets or sets the body local variables.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> EntityDeclarationBlocks { get; set; }

        /// <summary>
        /// Gets or sets the body instructions.
        /// </summary>
        public virtual IBlockList<Instruction> BodyInstructionBlocks { get; set; }

        /// <summary>
        /// Gets or sets the body exception handlers.
        /// </summary>
        public virtual IBlockList<ExceptionHandler> ExceptionHandlerBlocks { get; set; }
    }
}
