#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using Easly;

    public interface IPrecursorInstruction : IInstruction
    {
        IOptionalReference<IObjectType> AncestorType { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class PrecursorInstruction : Instruction, IPrecursorInstruction
    {
        public virtual IOptionalReference<IObjectType> AncestorType { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
