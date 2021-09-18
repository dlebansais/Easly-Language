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
        /// Gets or sets the typedef name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the typedef type.
        /// </summary>
        public virtual ObjectType DefinedType { get; set; } = null!;
    }
}
