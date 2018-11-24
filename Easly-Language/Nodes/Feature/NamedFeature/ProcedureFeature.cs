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
