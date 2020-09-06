#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IRaiseEventInstruction : IInstruction
    {
        IIdentifier QueryIdentifier { get; }
        EventType Event { get; }
    }

    [System.Serializable]
    public class RaiseEventInstruction : Instruction, IRaiseEventInstruction
    {
        public virtual IIdentifier QueryIdentifier { get; set; }
        public virtual EventType Event { get; set; }
    }
}
