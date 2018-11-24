namespace BaseNode
{
    public interface IExceptionHandler : INode
    {
        IIdentifier ExceptionIdentifier { get; }
        IScope Instructions { get; }
    }

    [System.Serializable]
    public class ExceptionHandler : Node, IExceptionHandler
    {
        public virtual IIdentifier ExceptionIdentifier { get; set; }
        public virtual IScope Instructions { get; set; }
    }
}
