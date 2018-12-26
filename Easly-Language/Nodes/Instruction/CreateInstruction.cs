using Easly;

namespace BaseNode
{
    public interface ICreateInstruction : IInstruction
    {
        IIdentifier EntityIdentifier { get; }
        IIdentifier CreationRoutineIdentifier { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
        IOptionalReference<IQualifiedName> Processor { get; }
    }

    [System.Serializable]
    public class CreateInstruction : Instruction, ICreateInstruction
    {
        public virtual IIdentifier EntityIdentifier { get; set; }
        public virtual IIdentifier CreationRoutineIdentifier { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
        public virtual IOptionalReference<IQualifiedName> Processor { get; set; }
    }
}
