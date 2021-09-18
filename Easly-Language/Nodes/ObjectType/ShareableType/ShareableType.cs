namespace BaseNode
{
    /// <summary>
    /// Represents any shareable type.
    /// </summary>
    public abstract class ShareableType : ObjectType
    {
        /// <summary>
        /// Gets or sets how the type is shared.
        /// </summary>
        public virtual SharingType Sharing { get; set; }
    }
}
