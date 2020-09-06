#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

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
