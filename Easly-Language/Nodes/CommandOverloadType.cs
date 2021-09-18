namespace BaseNode
{
    /// <summary>
    /// Represents a command overload type in a type definition.
    /// /Doc/Nodes/CommandOverloadType.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CommandOverloadType : Node
    {
        /// <summary>
        /// Gets or sets the overload parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the overload accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; }

        /// <summary>
        /// Gets or sets requirements.
        /// </summary>
        public virtual IBlockList<Assertion> RequireBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets guaranties.
        /// </summary>
        public virtual IBlockList<Assertion> EnsureBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets exception handlers.
        /// </summary>
        public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; } = null!;
    }
}
