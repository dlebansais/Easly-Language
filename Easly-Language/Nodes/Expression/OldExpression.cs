#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IOldExpression : IExpression
    {
        IQualifiedName Query { get; }
    }

    [System.Serializable]
    public class OldExpression : Expression, IOldExpression
    {
        public virtual IQualifiedName Query { get; set; }
    }
}
