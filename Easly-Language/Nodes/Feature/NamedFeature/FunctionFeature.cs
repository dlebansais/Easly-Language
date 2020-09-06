#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IFunctionFeature : INamedFeature
    {
        OnceChoice Once { get; }
        IBlockList<IQueryOverload, QueryOverload> OverloadBlocks { get; }
    }

    [System.Serializable]
    public class FunctionFeature : NamedFeature, IFunctionFeature
    {
        public virtual OnceChoice Once { get; set; }
        public virtual IBlockList<IQueryOverload, QueryOverload> OverloadBlocks { get; set; }
    }
}
