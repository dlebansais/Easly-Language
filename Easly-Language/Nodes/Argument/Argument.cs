#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public interface IArgument : INode
    {
    }

    public abstract class Argument : Node, IArgument
    {
    }
}
