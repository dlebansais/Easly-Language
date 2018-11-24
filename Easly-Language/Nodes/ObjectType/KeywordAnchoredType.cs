namespace BaseNode
{
    public interface IKeywordAnchoredType : IObjectType
    {
        Keyword Anchor { get; }
    }

    [System.Serializable]
    public class KeywordAnchoredType : ObjectType, IKeywordAnchoredType
    {
        public virtual Keyword Anchor { get; set; }
    }
}
