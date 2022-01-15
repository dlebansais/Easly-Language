namespace BaseNode;

/// <summary>
/// Represents any type.
/// </summary>
public abstract class ObjectType : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    internal ObjectType(Document documentation)
        : base(documentation)
    {
    }
}
