namespace BaseNode
{
    using Easly;

    public interface IClass : INode
    {
        IName EntityName { get; }
        IOptionalReference<IIdentifier> FromIdentifier { get; }
        CopySemantic CopySpecification { get; }
        CloneableStatus Cloneable { get; }
        ComparableStatus Comparable { get; }
        bool IsAbstract { get; }
        IBlockList<IImport, Import> ImportBlocks { get; }
        IBlockList<IGeneric, Generic> GenericBlocks { get; }
        IBlockList<IExport, Export> ExportBlocks { get; }
        IBlockList<ITypedef, Typedef> TypedefBlocks { get; }
        IBlockList<IInheritance, Inheritance> InheritanceBlocks { get; }
        IBlockList<IDiscrete, Discrete> DiscreteBlocks { get; }
        IBlockList<IClassReplicate, ClassReplicate> ClassReplicateBlocks { get; }
        IBlockList<IFeature, Feature> FeatureBlocks { get; }
        IBlockList<IIdentifier, Identifier> ConversionBlocks { get; }
        IBlockList<IAssertion, Assertion> InvariantBlocks { get; }
        System.Guid ClassGuid { get; }
        string ClassPath { get; }
    }

    [System.Serializable]
    public class Class : Node, IClass
    {
        public virtual IName EntityName { get; set; }
        public virtual IOptionalReference<IIdentifier> FromIdentifier { get; set; }
        public virtual CopySemantic CopySpecification { get; set; }
        public virtual CloneableStatus Cloneable { get; set; }
        public virtual ComparableStatus Comparable { get; set; }
        public virtual bool IsAbstract { get; set; }
        public virtual IBlockList<IImport, Import> ImportBlocks { get; set; }
        public virtual IBlockList<IGeneric, Generic> GenericBlocks { get; set; }
        public virtual IBlockList<IExport, Export> ExportBlocks { get; set; }
        public virtual IBlockList<ITypedef, Typedef> TypedefBlocks { get; set; }
        public virtual IBlockList<IInheritance, Inheritance> InheritanceBlocks { get; set; }
        public virtual IBlockList<IDiscrete, Discrete> DiscreteBlocks { get; set; }
        public virtual IBlockList<IClassReplicate, ClassReplicate> ClassReplicateBlocks { get; set; }
        public virtual IBlockList<IFeature, Feature> FeatureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ConversionBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> InvariantBlocks { get; set; }
        public virtual System.Guid ClassGuid { get; set; }
        public virtual string ClassPath { get; set; }
    }
}
