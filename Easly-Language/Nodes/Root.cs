#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace BaseNode
{
    using System.Collections.Generic;

    public interface IRoot : INode
    {
        IBlockList<IClass, Class> ClassBlocks { get; }
        IBlockList<ILibrary, Library> LibraryBlocks { get; }
        IList<IGlobalReplicate> Replicates { get; }
    }

    [System.Serializable]
    public class Root : Node, IRoot
    {
        public virtual IBlockList<IClass, Class> ClassBlocks { get; set; }
        public virtual IBlockList<ILibrary, Library> LibraryBlocks { get; set; }
        public virtual IList<IGlobalReplicate> Replicates { get; set; }
    }
}
