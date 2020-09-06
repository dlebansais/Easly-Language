#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface INewExpression : IExpression
    {
        IQualifiedName Object { get; }
    }

    [System.Serializable]
    public class NewExpression : Expression, INewExpression
    {
        public virtual IQualifiedName Object { get; set; }
    }
}
