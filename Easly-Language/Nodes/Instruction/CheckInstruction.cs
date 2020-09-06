#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface ICheckInstruction : IInstruction
    {
        IExpression BooleanExpression { get; }
    }

    [System.Serializable]
    public class CheckInstruction : Instruction, ICheckInstruction
    {
        public virtual IExpression BooleanExpression { get; set; }
    }
}
