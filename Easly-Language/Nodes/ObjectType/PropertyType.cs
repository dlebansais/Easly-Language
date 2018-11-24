namespace BaseNode
{
    public interface IPropertyType : IObjectType
    {
        IObjectType BaseType { get; }
        IObjectType EntityType { get; }
        UtilityType PropertyKind { get; }
        IBlockList<IAssertion, Assertion> GetEnsureBlocks { get; }
        IBlockList<IIdentifier, Identifier> GetExceptionIdentifierBlocks { get; }
        IBlockList<IAssertion, Assertion> SetRequireBlocks { get; }
        IBlockList<IIdentifier, Identifier> SetExceptionIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class PropertyType : ObjectType, IPropertyType
    {
        public virtual IObjectType BaseType { get; set; }
        public virtual IObjectType EntityType { get; set; }
        public virtual UtilityType PropertyKind { get; set; }
        public virtual IBlockList<IAssertion, Assertion> GetEnsureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> GetExceptionIdentifierBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> SetRequireBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> SetExceptionIdentifierBlocks { get; set; }
    }
}
