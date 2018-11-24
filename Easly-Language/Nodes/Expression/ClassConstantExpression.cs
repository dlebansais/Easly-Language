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
