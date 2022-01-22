namespace BaseNode;

/// <summary>
/// Represents a type definition
/// /Doc/Nodes/Typedef.md explains the semantic.
/// </summary>
[System.Serializable]
public class Typedef : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Typedef()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        EntityName = default!;
        DefinedType = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Typedef"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityName">The typedef name.</param>
    /// <param name="definedType">The typedef type.</param>
    internal Typedef(Document documentation, Name entityName, ObjectType definedType)
        : base(documentation)
    {
        EntityName = entityName;
        DefinedType = definedType;
    }

    /// <summary>
    /// Gets or sets the typedef name.
    /// </summary>
    public virtual Name EntityName { get; set; }

    /// <summary>
    /// Gets or sets the typedef type.
    /// </summary>
    public virtual ObjectType DefinedType { get; set; }
}
