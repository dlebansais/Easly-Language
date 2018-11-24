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
