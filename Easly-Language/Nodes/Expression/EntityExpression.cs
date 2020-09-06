#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IEntityExpression : IExpression
    {
        IQualifiedName Query { get; }
    }

    [System.Serializable]
    public class EntityExpression : Expression, IEntityExpression
    {
        public virtual IQualifiedName Query { get; set; }
    }
}
