namespace BaseNode;

/// <summary>
/// Import type.
/// </summary>
public enum ImportType
{
    /// <summary>
    /// Latest version.
    /// </summary>
    Latest,

    /// <summary>
    /// One particular version and no other.
    /// </summary>
    Strict,

    /// <summary>
    /// Any version with a compatible interface.
    /// </summary>
    Stable,
}
