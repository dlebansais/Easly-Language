using System.Collections.Generic;

namespace BaseNode
{
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
