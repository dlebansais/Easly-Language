namespace BaseNode
{
    public interface IContinuation : INode
    {
        IScope Instructions { get; }
        IBlockList<IInstruction, Instruction> CleanupBlocks { get; }
    }

    [System.Serializable]
    public class Continuation : Node, IContinuation
    {
        public virtual IScope Instructions { get; set; }
        public virtual IBlockList<IInstruction, Instruction> CleanupBlocks { get; set; }
    }
}
