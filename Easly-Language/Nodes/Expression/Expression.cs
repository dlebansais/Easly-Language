namespace BaseNode
{
    public interface IExpression : INode
    {
    }

    public abstract class Expression : Node, IExpression
    {
    }
}
