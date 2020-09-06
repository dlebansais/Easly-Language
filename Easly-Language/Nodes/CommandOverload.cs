#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    public interface ICommandOverload : INode
    {
        IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; }
        ParameterEndStatus ParameterEnd { get; }
        IBody CommandBody { get; }
    }

    [System.Serializable]
    public class CommandOverload : Node, ICommandOverload
    {
        public virtual IBlockList<IEntityDeclaration, EntityDeclaration> ParameterBlocks { get; set; }
        public virtual ParameterEndStatus ParameterEnd { get; set; }
        public virtual IBody CommandBody { get; set; }
    }
}
