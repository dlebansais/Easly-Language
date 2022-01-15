namespace BaseNode;

using Easly;

/// <summary>
/// Represents the call of a precursor.
/// /Doc/Nodes/Instruction/PrecursorInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class PrecursorInstruction : Instruction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrecursorInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="ancestorType">The type where to find the precursor.</param>
    /// <param name="argumentBlocks">The call arguments.</param>
    internal PrecursorInstruction(Document documentation, IOptionalReference<ObjectType> ancestorType, IBlockList<Argument> argumentBlocks)
        : base(documentation)
    {
        AncestorType = ancestorType;
        ArgumentBlocks = argumentBlocks;
    }

    /// <summary>
    /// Gets or sets the type where to find the precursor.
    /// </summary>
    public virtual IOptionalReference<ObjectType> AncestorType { get; set; }

    /// <summary>
    /// Gets or sets the call arguments.
    /// </summary>
    public virtual IBlockList<Argument> ArgumentBlocks { get; set; }
}
