#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
