using Easly;

namespace BaseNode
{
    public interface IPrecursorInstruction : IInstruction
    {
        OptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class PrecursorInstruction : Instruction, IPrecursorInstruction
    {
        public virtual OptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
