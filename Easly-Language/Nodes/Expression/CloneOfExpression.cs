#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface ICloneOfExpression : IExpression
    {
        CloneType Type { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class CloneOfExpression : Expression, ICloneOfExpression
    {
        public virtual CloneType Type { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
