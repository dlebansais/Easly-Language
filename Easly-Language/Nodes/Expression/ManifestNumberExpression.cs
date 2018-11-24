namespace BaseNode
{
    public interface IManifestNumberExpression : IExpression
    {
        string Text { get; }
    }

    [System.Serializable]
    public class ManifestNumberExpression : Expression, IManifestNumberExpression
    {
        public virtual string Text { get; set; }
    }
}
