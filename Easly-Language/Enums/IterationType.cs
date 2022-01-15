namespace BaseNode;

/// <summary>
/// Iteration type.
/// </summary>
public enum IterationType
{
    /// <summary>
    /// All iterators are iterated at the same time (N iterations).
    /// </summary>
    Single,

    /// <summary>
    /// Nested iterators (N ^ P iterations).
    /// </summary>
    Nested,
}
