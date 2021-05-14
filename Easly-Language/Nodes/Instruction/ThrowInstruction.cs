#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public interface IThrowInstruction : IInstruction
    {
        IObjectType ExceptionType { get; }
        IIdentifier CreationRoutine { get; }
        IBlockList<IArgument, Argument> ArgumentBlocks { get; }
    }

    [System.Serializable]
    public class ThrowInstruction : Instruction, IThrowInstruction
    {
        public virtual IObjectType ExceptionType { get; set; }
        public virtual IIdentifier CreationRoutine { get; set; }
        public virtual IBlockList<IArgument, Argument> ArgumentBlocks { get; set; }
    }
}
