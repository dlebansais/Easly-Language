namespace BaseNode;

/// <summary>
/// Represents a command overload type in a type definition.
/// /Doc/Nodes/CommandOverloadType.md explains the semantic.
/// </summary>
[System.Serializable]
public class CommandOverloadType : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public CommandOverloadType()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ParameterBlocks = default!;
        ParameterEnd = default!;
        RequireBlocks = default!;
        EnsureBlocks = default!;
        ExceptionIdentifierBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandOverloadType"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parameterBlocks">The overload parameters.</param>
    /// <param name="parameterEnd">Whether the overload accepts extra parameters.</param>
    /// <param name="requireBlocks">The requirements.</param>
    /// <param name="ensureBlocks">The guarantees.</param>
    /// <param name="exceptionIdentifierBlocks">The exception handlers.</param>
    internal CommandOverloadType(Document documentation, IBlockList<EntityDeclaration> parameterBlocks, ParameterEndStatus parameterEnd, IBlockList<Assertion> requireBlocks, IBlockList<Assertion> ensureBlocks, IBlockList<Identifier> exceptionIdentifierBlocks)
        : base(documentation)
    {
        ParameterBlocks = parameterBlocks;
        ParameterEnd = parameterEnd;
        RequireBlocks = requireBlocks;
        EnsureBlocks = ensureBlocks;
        ExceptionIdentifierBlocks = exceptionIdentifierBlocks;
    }

    /// <summary>
    /// Gets or sets the overload parameters.
    /// </summary>
    public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; }

    /// <summary>
    /// Gets or sets whether the overload accepts extra parameters.
    /// </summary>
    public virtual ParameterEndStatus ParameterEnd { get; set; }

    /// <summary>
    /// Gets or sets the requirements.
    /// </summary>
    public virtual IBlockList<Assertion> RequireBlocks { get; set; }

    /// <summary>
    /// Gets or sets the guarantees.
    /// </summary>
    public virtual IBlockList<Assertion> EnsureBlocks { get; set; }

    /// <summary>
    /// Gets or sets the exception handlers.
    /// </summary>
    public virtual IBlockList<Identifier> ExceptionIdentifierBlocks { get; set; }
}
