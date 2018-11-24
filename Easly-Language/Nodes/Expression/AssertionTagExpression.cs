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
