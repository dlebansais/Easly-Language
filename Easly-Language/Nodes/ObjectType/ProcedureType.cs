namespace BaseNode;

/// <summary>
/// Represents a procedure type.
/// /Doc/Nodes/Type/ProcedureType.md explains the semantic.
/// </summary>
[System.Serializable]
public class ProcedureType : ObjectType
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public ProcedureType()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        BaseType = default!;
        OverloadBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcedureType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="baseType">The base type.</param>
    /// <param name="overloadBlocks">The list of overloads.</param>
    internal ProcedureType(Document documentation, ObjectType baseType, IBlockList<CommandOverloadType> overloadBlocks)
        : base(documentation)
    {
        BaseType = baseType;
        OverloadBlocks = overloadBlocks;
    }

    /// <summary>
    /// Gets or sets the base type.
    /// </summary>
    public virtual ObjectType BaseType { get; set; }

    /// <summary>
    /// Gets or sets the list of overloads.
    /// </summary>
    public virtual IBlockList<CommandOverloadType> OverloadBlocks { get; set; }
}
