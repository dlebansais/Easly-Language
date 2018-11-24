namespace BaseNode
{
    public interface IManifestStringExpression : IExpression
    {
        string Text { get; }
    }

    [System.Serializable]
    public class ManifestStringExpression : Expression, IManifestStringExpression
    {
        public virtual string Text { get; set; }
    }
}
