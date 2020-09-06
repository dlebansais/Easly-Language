#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
