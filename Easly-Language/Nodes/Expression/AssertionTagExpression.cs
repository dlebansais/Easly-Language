#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IAssertionTagExpression : IExpression
    {
        IIdentifier TagIdentifier { get; }
    }

    [System.Serializable]
    public class AssertionTagExpression : Expression, IAssertionTagExpression
    {
        public virtual IIdentifier TagIdentifier { get; set; }
    }
}
