namespace BaseNode
{
    public interface ITupleType : IShareableType
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; }
    }

    [System.Serializable]
    public class TupleType : ShareableType, ITupleType
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> EntityDeclarationBlocks { get; set; }
    }
}
