namespace BaseNode
{
    /// <summary>
    /// Represents a procedure feature.
    /// /Doc/Nodes/Feature/ProcedureFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ProcedureFeature : NamedFeature
    {
        /// <summary>
        /// Gets or sets the list of overloads.
        /// </summary>
        public virtual IBlockList<CommandOverload> OverloadBlocks { get; set; } = null!;
    }
}
