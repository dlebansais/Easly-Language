namespace BaseNode
{
    public interface ICloneOfExpression : IExpression
    {
        CloneType Type { get; }
        IExpression Source { get; }
    }

    [System.Serializable]
    public class CloneOfExpression : Expression, ICloneOfExpression
    {
        public virtual CloneType Type { get; set; }
        public virtual IExpression Source { get; set; }
    }
}
