namespace BaseNode
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the root of a program.
    /// /Doc/Nodes/Root.md explains the semantic.
    /// </summary>
    [System.Serializable]
    public class Root : Node
    {
        /// <summary>
        /// Gets or sets the list of classes in the program.
        /// </summary>
        public virtual IBlockList<Class> ClassBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of libraries.
        /// </summary>
        public virtual IBlockList<Library> LibraryBlocks { get; set; } = null!;

        /// <summary>
        /// Gets or sets the global replicates.
        /// </summary>
        public virtual IList<GlobalReplicate> Replicates { get; set; } = null!;
    }
}
