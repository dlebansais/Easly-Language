#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
