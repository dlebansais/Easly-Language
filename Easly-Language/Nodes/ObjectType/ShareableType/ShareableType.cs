namespace BaseNode;

/// <summary>
/// Represents any shareable type.
/// </summary>
public abstract class ShareableType : ObjectType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShareableType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="sharing">How the type is shared.</param>
    internal ShareableType(Document documentation, SharingType sharing)
        : base(documentation)
    {
        Sharing = sharing;
    }

    /// <summary>
    /// Gets or sets how the type is shared.
    /// </summary>
    public virtual SharingType Sharing { get; set; }
}
