#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    public interface ITypedef : INode
    {
        IName EntityName { get; }
        IObjectType DefinedType { get; }
    }

    [System.Serializable]
    public class Typedef : Node, ITypedef
    {
        public virtual IName EntityName { get; set; }
        public virtual IObjectType DefinedType { get; set; }
    }
}
