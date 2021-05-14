#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

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
