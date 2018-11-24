namespace BaseNode
{
    public interface ICreationFeature : INamedFeature
    {
        IBlockList<ICommandOverload, CommandOverload> OverloadBlocks { get; }
    }

    [System.Serializable]
    public class CreationFeature : NamedFeature, ICreationFeature
    {
        public virtual IBlockList<ICommandOverload, CommandOverload> OverloadBlocks { get; set; }
    }
}
