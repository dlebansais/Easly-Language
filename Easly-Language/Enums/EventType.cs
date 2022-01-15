namespace BaseNode;

/// <summary>
/// Event type.
/// </summary>
public enum EventType
{
    /// <summary>
    /// Event can be reset after being signaled.
    /// </summary>
    Single,

    /// <summary>
    /// Event can signaled only once and never reset.
    /// </summary>
    Forever,
}
