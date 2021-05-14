#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
