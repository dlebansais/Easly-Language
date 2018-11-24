namespace BaseNode
{
    public interface IConditional : INode
    {
        IExpression BooleanExpression { get; }
        IScope Instructions { get; }
    }

    [System.Serializable]
    public class Conditional : Node, IConditional
    {
        public virtual IExpression BooleanExpression { get; set; }
        public virtual IScope Instructions { get; set; }
    }
}