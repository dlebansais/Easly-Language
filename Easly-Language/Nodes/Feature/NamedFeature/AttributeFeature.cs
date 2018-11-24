namespace BaseNode
{
    public interface IAttributeFeature : INamedFeature
    {
        IObjectType EntityType { get; }
        IBlockList<IAssertion, Assertion> EnsureBlocks { get; }
    }

    [System.Serializable]
    public class AttributeFeature : NamedFeature, IAttributeFeature
    {
        public virtual IObjectType EntityType { get; set; }
        public virtual IBlockList<IAssertion, Assertion> EnsureBlocks { get; set; }
    }
}
