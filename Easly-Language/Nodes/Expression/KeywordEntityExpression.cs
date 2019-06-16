namespace BaseNode
{
    public interface IKeywordEntityExpression : IExpression
    {
        Keyword Value { get; }
    }

    [System.Serializable]
    public class KeywordEntityExpression : Expression, IKeywordEntityExpression
    {
        public virtual Keyword Value { get; set; }
    }
}
