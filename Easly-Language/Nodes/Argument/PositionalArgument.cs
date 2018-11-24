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
