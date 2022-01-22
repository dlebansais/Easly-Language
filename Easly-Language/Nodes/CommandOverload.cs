namespace BaseNode;

/// <summary>
/// Represents a command overload in a feature.
/// /Doc/Nodes/CommandOverload.md explains the semantic.
/// </summary>
[System.Serializable]
public class CommandOverload : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public CommandOverload()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        ParameterBlocks = default!;
        ParameterEnd = default!;
        CommandBody = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandOverload"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="parameterBlocks">The command parameters.</param>
    /// <param name="parameterEnd">Whether the command accepts extra parameters.</param>
    /// <param name="commandBody">The command body.</param>
    internal CommandOverload(Document documentation, IBlockList<EntityDeclaration> parameterBlocks, ParameterEndStatus parameterEnd, Body commandBody)
        : base(documentation)
    {
        ParameterBlocks = parameterBlocks;
        ParameterEnd = parameterEnd;
        CommandBody = commandBody;
    }

    /// <summary>
    /// Gets or sets the command parameters.
    /// </summary>
    public virtual IBlockList<EntityDeclaration> ParameterBlocks { get; set; }

    /// <summary>
    /// Gets or sets whether the command accepts extra parameters.
    /// </summary>
    public virtual ParameterEndStatus ParameterEnd { get; set; }

    /// <summary>
    /// Gets or sets the command body.
    /// </summary>
    public virtual Body CommandBody { get; set; }
}
