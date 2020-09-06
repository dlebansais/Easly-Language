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
