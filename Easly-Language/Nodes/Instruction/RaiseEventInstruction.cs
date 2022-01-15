namespace BaseNode
{
    /// <summary>
    /// Represents the instruction raising an event.
    /// /Doc/Nodes/Instruction/RaiseEventInstruction.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class RaiseEventInstruction : Instruction
    {
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
}
