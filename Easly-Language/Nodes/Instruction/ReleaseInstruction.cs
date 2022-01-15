namespace BaseNode;

/// <summary>
/// Represents the instruction releasing a reference.
/// /Doc/Nodes/Instruction/ReleaseInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class ReleaseInstruction : Instruction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReleaseInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="entityName">The released reference.</param>
    internal ReleaseInstruction(Document documentation, QualifiedName entityName)
        : base(documentation)
    {
        EntityName = entityName;
    }

    /// <summary>
    /// Gets or sets the released reference.
    /// </summary>
    public virtual QualifiedName EntityName { get; set; }
}
