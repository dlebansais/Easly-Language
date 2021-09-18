namespace BaseNode
{
    using Easly;

    /// <summary>
    /// Represents a precursor body.
    /// /Doc/Nodes/Body/PrecursorBody.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class PrecursorBody : Body
    {
        /// <summary>
        /// Gets or sets the ancestor type in case of multiple ancestors.
        /// </summary>
        public virtual IOptionalReference<ObjectType> AncestorType { get; set; } = null!;
    }
}
