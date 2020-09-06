#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IKeywordAssignmentInstruction : IInstruction
    {
        Keyword Destination { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class KeywordAssignmentInstruction : Instruction, IKeywordAssignmentInstruction
    {
        public virtual Keyword Destination { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
