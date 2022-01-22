namespace BaseNode;

/// <summary>
/// Represents the instruction raising an event.
/// /Doc/Nodes/Instruction/RaiseEventInstruction.md explains the semantic.
/// </summary>
[System.Serializable]
public class RaiseEventInstruction : Instruction
{
#if !NO_PARAMETERLESS_CONSTRUCTOR
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public RaiseEventInstruction()
#pragma warning restore SA1600 // Elements should be documented
        : base(default!)
    {
        QueryIdentifier = default!;
        Event = default!;
    }
#endif
    /// <summary>
    /// Initializes a new instance of the <see cref="RaiseEventInstruction"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="queryIdentifier">The event identifier.</param>
    /// <param name="event">Whether the event is single or forever.</param>
    internal RaiseEventInstruction(Document documentation, Identifier queryIdentifier, EventType @event)
        : base(documentation)
    {
        QueryIdentifier = queryIdentifier;
        Event = @event;
    }

    /// <summary>
    /// Gets or sets the event identifier.
    /// </summary>
    public virtual Identifier QueryIdentifier { get; set; }

    /// <summary>
    /// Gets or sets whether the event is single or forever.
    /// </summary>
    public virtual EventType Event { get; set; }
}
