namespace BaseNode
{
    public interface ICommandInstruction : IInstruction
    {
        IQualifiedName Command { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class CommandInstruction : Instruction, ICommandInstruction
    {
        public virtual IQualifiedName Command { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
