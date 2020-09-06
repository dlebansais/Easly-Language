#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
