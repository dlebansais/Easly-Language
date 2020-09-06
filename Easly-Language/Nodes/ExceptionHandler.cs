#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
