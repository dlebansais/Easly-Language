namespace BaseNode
{
    public interface IInstruction : INode
    {
    }

    public abstract class Instruction : Node, IInstruction
    {
    }
}
