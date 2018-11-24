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
