namespace BaseNode
{
    /// <summary>
    /// Represents any feature with a name.
    /// </summary>
    public abstract class NamedFeature : Feature
    {
        /// <summary>
        /// Gets or sets the feature name.
        /// </summary>
        public virtual Name EntityName { get; set; } = null!;
    }
}
