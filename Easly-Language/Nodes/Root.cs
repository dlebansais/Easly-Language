#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable SA1600 // Elements should be documented

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
