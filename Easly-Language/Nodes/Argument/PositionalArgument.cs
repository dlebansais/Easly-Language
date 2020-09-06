#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IPositionalArgument : IArgument
    {
        IExpression Source { get; }
    }

    [System.Serializable]
    public class PositionalArgument : Argument, IPositionalArgument
    {
        public virtual IExpression Source { get; set; }
    }
}
