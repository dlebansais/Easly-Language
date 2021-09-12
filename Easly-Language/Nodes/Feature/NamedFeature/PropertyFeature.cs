#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class PropertyFeature : NamedFeature
    {
        public virtual ObjectType EntityType { get; set; }
        public virtual UtilityType PropertyKind { get; set; }
        public virtual BlockList<Identifier> ModifiedQueryBlocks { get; set; }
        public virtual OptionalReference<Body> GetterBody { get; set; }
        public virtual OptionalReference<Body> SetterBody { get; set; }
    }
}
