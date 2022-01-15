namespace BaseNode
{
    /// <summary>
    /// Represents a type definition
    /// /Doc/Nodes/Typedef.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Typedef : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Typedef"/> class.
        /// </summary>
        /// <param name="documentation">The node documentation.</param>
        /// <param name="entityName">The typedef name.</param>
        /// <param name="definedType">The typedef type.</param>
        internal Typedef(Document documentation, Name entityName, ObjectType definedType)
            : base(documentation)
        {
            EntityName = entityName;
            DefinedType = definedType;
        }

        /// <summary>
        /// Gets or sets the typedef name.
        /// </summary>
        public virtual Name EntityName { get; set; }

        /// <summary>
        /// Gets or sets the typedef type.
        /// </summary>
        public virtual ObjectType DefinedType { get; set; }
    }
}
