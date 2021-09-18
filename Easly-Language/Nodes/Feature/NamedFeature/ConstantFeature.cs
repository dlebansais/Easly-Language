namespace BaseNode
{
    /// <summary>
    /// Represents a constant feature.
    /// /Doc/Nodes/Feature/ConstantFeature.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class ConstantFeature : NamedFeature
    {
        /// <summary>
        /// Gets or sets the constant type.
        /// </summary>
        public virtual ObjectType EntityType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public virtual Expression ConstantValue { get; set; } = null!;
    }
}
