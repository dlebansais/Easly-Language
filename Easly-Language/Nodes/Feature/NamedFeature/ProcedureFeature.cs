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
        /// Initializes a new instance of the <see cref="ProcedureFeature"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="exportIdentifier">The export to which this feature belongs.</param>
        /// <param name="export">The export type.</param>
        /// <param name="entityName">The procedure name.</param>
        /// <param name="overloadBlocks">The list of overloads.</param>
        internal ProcedureFeature(Document documentation, Identifier exportIdentifier, ExportStatus export, Name entityName, IBlockList<CommandOverload> overloadBlocks)
            : base(documentation, exportIdentifier, export, entityName)
        {
            OverloadBlocks = overloadBlocks;
        }

        /// <summary>
        /// Gets or sets the list of overloads.
        /// </summary>
        public virtual IBlockList<CommandOverload> OverloadBlocks { get; set; }
    }
}
