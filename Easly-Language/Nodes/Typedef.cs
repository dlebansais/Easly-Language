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
