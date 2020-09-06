#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IConstraint : INode
    {
        IObjectType ParentType { get; }
        IBlockList<IRename, Rename> RenameBlocks { get; }
    }

    [System.Serializable]
    public class Constraint : Node, IConstraint
    {
        public virtual IObjectType ParentType { get; set; }
        public virtual IBlockList<IRename, Rename> RenameBlocks { get; set; }
    }
}
