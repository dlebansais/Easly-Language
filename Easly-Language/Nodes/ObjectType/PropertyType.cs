#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    [System.Serializable]
    public class PropertyType : ObjectType
    {
        public virtual ObjectType BaseType { get; set; }
        public virtual ObjectType EntityType { get; set; }
        public virtual UtilityType PropertyKind { get; set; }
        public virtual BlockList<Assertion> GetEnsureBlocks { get; set; }
        public virtual BlockList<Identifier> GetExceptionIdentifierBlocks { get; set; }
        public virtual BlockList<Assertion> SetRequireBlocks { get; set; }
        public virtual BlockList<Identifier> SetExceptionIdentifierBlocks { get; set; }
    }
}
