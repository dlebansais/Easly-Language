#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IPositionalTypeArgument : ITypeArgument
    {
        IObjectType Source { get; }
    }

    [System.Serializable]
    public class PositionalTypeArgument : TypeArgument, IPositionalTypeArgument
    {
        public virtual IObjectType Source { get; set; }
    }
}
