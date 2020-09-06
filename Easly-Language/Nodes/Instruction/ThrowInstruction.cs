#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
