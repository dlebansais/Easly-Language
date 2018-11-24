namespace BaseNode
{
    public interface IManifestCharacterExpression : IExpression
    {
        string Text { get; }
    }

    [System.Serializable]
    public class ManifestCharacterExpression : Expression, IManifestCharacterExpression
    {
        public virtual string Text { get; set; }
    }
}
