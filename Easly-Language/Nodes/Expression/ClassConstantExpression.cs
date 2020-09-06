#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IClassConstantExpression : IExpression
    {
        IIdentifier ClassIdentifier { get; }
        IIdentifier ConstantIdentifier { get; }
    }

    [System.Serializable]
    public class ClassConstantExpression : Expression, IClassConstantExpression
    {
        public virtual IIdentifier ClassIdentifier { get; set; }
        public virtual IIdentifier ConstantIdentifier { get; set; }
    }
}
