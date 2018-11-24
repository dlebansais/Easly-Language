namespace BaseNode
{
    public interface IIndexerType : IObjectType
    {
        IObjectType BaseType { get; }
        IObjectType EntityType { get; }
        IBlockList<IEntityDeclaration, EntityDeclaration> IndexParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        UtilityType IndexerKind { get; }
        IBlockList<IAssertion, Assertion> GetRequireBlocks { get; }
        IBlockList<IAssertion, Assertion> GetEnsureBlocks { get; }
        IBlockList<IIdentifier, Identifier> GetExceptionIdentifierBlocks { get; }
        IBlockList<IAssertion, Assertion> SetRequireBlocks { get; }
        IBlockList<IAssertion, Assertion> SetEnsureBlocks { get; }
        IBlockList<IIdentifier, Identifier> SetExceptionIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class IndexerType : ObjectType, IIndexerType
    {
        public virtual IObjectType BaseType { get; set; }
        public virtual IObjectType EntityType { get; set; }
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> IndexParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual UtilityType IndexerKind { get; set; }
        public virtual IBlockList<IAssertion, Assertion> GetRequireBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> GetEnsureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> GetExceptionIdentifierBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> SetRequireBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> SetEnsureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> SetExceptionIdentifierBlocks { get; set; }
    }
}
