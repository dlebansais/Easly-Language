namespace BaseNode
{
    public interface ICommandOverloadType : INode
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        IBlockList<IAssertion, Assertion> RequireBlocks { get; }
        IBlockList<IAssertion, Assertion> EnsureBlocks { get; }
        IBlockList<IIdentifier, Identifier> ExceptionIdentifierBlocks { get; }
    }

    [System.Serializable]
    public class CommandOverloadType : Node, ICommandOverloadType
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBlockList<IAssertion, Assertion> RequireBlocks { get; set; }
        public virtual IBlockList<IAssertion, Assertion> EnsureBlocks { get; set; }
        public virtual IBlockList<IIdentifier, Identifier> ExceptionIdentifierBlocks { get; set; }
    }
}