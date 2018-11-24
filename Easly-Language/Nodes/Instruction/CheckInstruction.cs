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
