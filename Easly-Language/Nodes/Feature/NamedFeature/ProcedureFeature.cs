#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IProcedureFeature : INamedFeature
    {
        IBlockList<ICommandOverload, CommandOverload> OverloadBlocks { get; }
    }

    [System.Serializable]
    public class ProcedureFeature : NamedFeature, IProcedureFeature
    {
        public virtual IBlockList<ICommandOverload, CommandOverload> OverloadBlocks { get; set; }
    }
}
