namespace BaseNode
{
    public interface IScope : INode
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; }
        IBlockList<IInstruction, Instruction> InstructionBlocks { get; }
    }

    [System.Serializable]
    public class Scope : Node, IScope
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; set; }
        public virtual IBlockList<IInstruction, Instruction> InstructionBlocks { get; set; }
    }
}
