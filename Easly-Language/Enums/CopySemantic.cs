namespace BaseNode;

/// <summary>
/// Copy semantics.
/// </summary>
public enum CopySemantic
{
    /// <summary>
    /// Any type of copy.
    /// </summary>
    Any,

    /// <summary>
    /// Copy by reference.
    /// </summary>
    Reference,

    /// <summary>
    /// Copy by value.
    /// </summary>
    Value,
}
