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
