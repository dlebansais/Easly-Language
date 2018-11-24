namespace BaseNode
{
    public interface IKeywordExpression : IExpression
    {
        Keyword Value { get; }
    }

    [System.Serializable]
    public class KeywordExpression : Expression, IKeywordExpression
    {
        public virtual Keyword Value { get; set; }
    }
}
