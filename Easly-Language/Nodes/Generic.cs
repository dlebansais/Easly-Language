namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a generic in a generic type.
    /// /Doc/Nodes/Generic.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Generic : Node
    {
        /// <summary>
        /// Gets or sets the generic name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the generic default value.
        /// </summary>
        public virtual IOptionalReference<ObjectType> DefaultValue { get; set; } = null!;

        /// <summary>
        /// Gets or sets constraints for this generic.
        /// </summary>
        public virtual IBlockList<Constraint> ConstraintBlocks { get; set; } = null!;
    }
}
