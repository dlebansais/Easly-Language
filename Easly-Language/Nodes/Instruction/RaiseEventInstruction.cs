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
        /// Gets or sets the event identifier.
        /// </summary>
        public virtual Identifier QueryIdentifier { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether the event is single or forever.
        /// </summary>
        public virtual EventType Event { get; set; } = EventType.Single;
    }
}
