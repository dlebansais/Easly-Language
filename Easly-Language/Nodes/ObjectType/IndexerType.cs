#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    [System.Serializable]
    public class IndexerType : ObjectType
    {
        public virtual ObjectType BaseType { get; set; }
        public virtual ObjectType EntityType { get; set; }
        public virtual IBlockList<EntityDeclaration> IndexParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual UtilityType IndexerKind { get; set; }
        public virtual IBlockList<Assertion> GetRequireBlocks { get; set; }
        public virtual IBlockList<Assertion> GetEnsureBlocks { get; set; }
        public virtual IBlockList<Identifier> GetExceptionIdentifierBlocks { get; set; }
        public virtual IBlockList<Assertion> SetRequireBlocks { get; set; }
        public virtual IBlockList<Assertion> SetEnsureBlocks { get; set; }
        public virtual IBlockList<Identifier> SetExceptionIdentifierBlocks { get; set; }
    }
}
