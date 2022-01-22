namespace BaseNode;

/// <summary>
/// Represents a type with generics.
/// /Doc/Nodes/Type/GenericType.md explains the semantic.
/// </summary>
[System.Serializable]
public class GenericType : ShareableType
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public GenericType()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!, default!)
    {
        ClassIdentifier = default!;
        TypeArgumentBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="GenericType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="sharing">How the type is shared.</param>
    /// <param name="classIdentifier">The class identifier.</param>
    /// <param name="typeArgumentBlocks">The list of generic parameters.</param>
    internal GenericType(Document documentation, SharingType sharing, Identifier classIdentifier, IBlockList<TypeArgument> typeArgumentBlocks)
        : base(documentation, sharing)
    {
        ClassIdentifier = classIdentifier;
        TypeArgumentBlocks = typeArgumentBlocks;
    }

    /// <summary>
    /// Gets or sets the class identifier.
    /// </summary>
    public virtual Identifier ClassIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the list of generic parameters.
    /// </summary>
    public virtual IBlockList<TypeArgument> TypeArgumentBlocks { get; set; }
}
