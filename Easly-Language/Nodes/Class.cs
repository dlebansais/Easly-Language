#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

namespace BaseNode
{
    using Easly;

    [System.Serializable]
    public class Class : Node
    {
        public virtual Name EntityName { get; set; }
        public virtual OptionalReference<Identifier> FromIdentifier { get; set; }
        public virtual CopySemantic CopySpecification { get; set; }
        public virtual CloneableStatus Cloneable { get; set; }
        public virtual ComparableStatus Comparable { get; set; }
        public virtual bool IsAbstract { get; set; }
        public virtual BlockList<Import> ImportBlocks { get; set; }
        public virtual BlockList<Generic> GenericBlocks { get; set; }
        public virtual BlockList<Export> ExportBlocks { get; set; }
        public virtual BlockList<Typedef> TypedefBlocks { get; set; }
        public virtual BlockList<Inheritance> InheritanceBlocks { get; set; }
        public virtual BlockList<Discrete> DiscreteBlocks { get; set; }
        public virtual BlockList<ClassReplicate> ClassReplicateBlocks { get; set; }
        public virtual BlockList<Feature> FeatureBlocks { get; set; }
        public virtual BlockList<Identifier> ConversionBlocks { get; set; }
        public virtual BlockList<Assertion> InvariantBlocks { get; set; }
        public virtual System.Guid ClassGuid { get; set; }
        public virtual string ClassPath { get; set; }
    }
}
