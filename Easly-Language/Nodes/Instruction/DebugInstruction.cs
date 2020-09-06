#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IDebugInstruction : IInstruction
    {
        IScope Instructions { get; }
    }

    [System.Serializable]
    public class DebugInstruction : Instruction, IDebugInstruction
    {
        public virtual IScope Instructions { get; set; }
    }
}
