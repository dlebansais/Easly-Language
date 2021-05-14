#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public interface IEffectiveBody : IBody
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; }
        IBlockList<IInstruction, Instruction> BodyInstructionBlocks { get; }
        IBlockList<IExceptionHandler, ExceptionHandler> ExceptionHandlerBlocks { get; }
    }

    [System.Serializable]
    public class EffectiveBody : Body, IEffectiveBody
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; set; }
        public virtual IBlockList<IInstruction, Instruction> BodyInstructionBlocks { get; set; }
        public virtual IBlockList<IExceptionHandler, ExceptionHandler> ExceptionHandlerBlocks { get; set; }
    }
}
