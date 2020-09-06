#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
