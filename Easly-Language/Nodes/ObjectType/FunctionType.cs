namespace BaseNode
{
    public interface IFunctionType : IObjectType
    {
        IObjectType BaseType { get; }
        IBlockList<IQueryOverloadType, QueryOverloadType> OverloadBlocks { get; }
    }

    [System.Serializable]
    public class FunctionType : ObjectType, IFunctionType
    {
        public virtual IObjectType BaseType { get; set; }
        public virtual IBlockList<IQueryOverloadType, QueryOverloadType> OverloadBlocks { get; set; }
    }
}
