namespace BaseNode
{
    public interface IManifestNumericExpression : IExpression
    {
        string Text { get; }
    }

    [System.Serializable]
    public class ManifestNumericExpression : Expression, IManifestNumericExpression
    {
        public virtual string Text { get; set; }
    }
}
