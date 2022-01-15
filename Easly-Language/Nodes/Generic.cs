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
        /// Initializes a new instance of the <see cref="Generic"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="entityName">The generic name.</param>
        /// <param name="defaultValue">The generic default value.</param>
        /// <param name="constraintBlocks">The constraints for this generic.</param>
        internal Generic(Document documentation, Name entityName, IOptionalReference<ObjectType> defaultValue, IBlockList<Constraint> constraintBlocks)
            : base(documentation)
        {
            EntityName = entityName;
            DefaultValue = defaultValue;
            ConstraintBlocks = constraintBlocks;
        }

        /// <summary>
        /// Gets or sets the generic name.
        /// </summary>
        public virtual Name EntityName { get; set; }

        /// <summary>
        /// Gets or sets the generic default value.
        /// </summary>
        public virtual IOptionalReference<ObjectType> DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the constraints for this generic.
        /// </summary>
        public virtual IBlockList<Constraint> ConstraintBlocks { get; set; }
    }
}
