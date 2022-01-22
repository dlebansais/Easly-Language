namespace BaseNode;

using Easly;

/// <summary>
/// Represents a generic in a generic type.
/// /Doc/Nodes/Generic.md explains the semantic.
/// </summary>
[System.Serializable]
public class Generic : Node
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public Generic()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        EntityName = default!;
        DefaultValue = default!;
        ConstraintBlocks = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="Generic"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityName">The generic name.</param>
    /// <param name="defaultValue">The generic default value.</param>
    /// <param name="constraintBlocks">The constraints for this generic.</param>
    internal Generic(Document documentation, Name entityName, IOptionalReference<ObjectType> defaultValue, IBlockList<Constraint> constraintBlocks)
        : base(documentation)
    {
        EntityName = entityName;
        DefaultValue = defaultValue;
        ConstraintBlocks = constraintBlocks;
    }

    /// <summary>
    /// Gets or sets the generic name.
    /// </summary>
    public virtual Name EntityName { get; set; }

    /// <summary>
    /// Gets or sets the generic default value.
    /// </summary>
    public virtual IOptionalReference<ObjectType> DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets the constraints for this generic.
    /// </summary>
    public virtual IBlockList<Constraint> ConstraintBlocks { get; set; }
}
