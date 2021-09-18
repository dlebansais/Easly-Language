namespace BaseNode
{
    /// <summary>
    /// Represents a contraint in a generic definition.
    /// /Doc/Nodes/Constraint.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Constraint : Node
    {
        /// <summary>
        /// Gets or sets the constraint type.
        /// </summary>
        public virtual ObjectType ParentType { get; set; } = null!;

        /// <summary>
        /// Gets or sets rename statements to use for this constraint.
        /// </summary>
        public virtual IBlockList<Rename> RenameBlocks { get; set; } = null!;
    }
}
