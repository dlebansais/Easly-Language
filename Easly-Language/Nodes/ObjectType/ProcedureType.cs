#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface IProcedureType : IObjectType
    {
        IObjectType BaseType { get; }
        IBlockList<ICommandOverloadType, CommandOverloadType> OverloadBlocks { get; }
    }

    [System.Serializable]
    public class ProcedureType : ObjectType, IProcedureType
    {
        public virtual IObjectType BaseType { get; set; }
        public virtual IBlockList<ICommandOverloadType, CommandOverloadType> OverloadBlocks { get; set; }
    }
}
