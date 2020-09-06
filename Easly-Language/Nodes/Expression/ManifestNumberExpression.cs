#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
