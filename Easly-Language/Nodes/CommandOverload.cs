namespace BaseNode
{
    /// <summary>
    /// Represents a command overload in a feature.
    /// /Doc/Nodes/CommandOverload.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class CommandOverload : Node
    {
        /// <summary>
        /// Gets or sets the command parameters.
        /// </summary>
        public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the command accepts extra parameters.
        /// </summary>
        public virtual ParameterEndStatus ParameterEnd { get; set; }

        /// <summary>
        /// Gets or sets the command body.
        /// </summary>
        public virtual Body CommandBody { get; set; } = null!;
    }
}
