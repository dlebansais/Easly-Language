namespace BaseNode;

using System.Collections.Generic;

/// <summary>
/// Represents the root of a program.
/// /Doc/Nodes/Root.md explains the semantic.
/// </summary>
[System.Serializable]
public class Root : Node
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Root"/> class.
    /// </summary>
    /// <param name="documentation">The node documentation.</param>
    /// <param name="classBlocks">The list of classes in the program.</param>
    /// <param name="libraryBlocks">The list of libraries.</param>
    /// <param name="replicates">The global replicates.</param>
    internal Root(Document documentation, IBlockList<Class> classBlocks, IBlockList<Library> libraryBlocks, IList<GlobalReplicate> replicates)
        : base(documentation)
    {
        ClassBlocks = classBlocks;
        LibraryBlocks = libraryBlocks;
        Replicates = replicates;
    }

    /// <summary>
    /// Gets or sets the list of classes in the program.
    /// </summary>
    public virtual IBlockList<Class> ClassBlocks { get; set; }

    /// <summary>
    /// Gets or sets the list of libraries.
    /// </summary>
    public virtual IBlockList<Library> LibraryBlocks { get; set; }

    /// <summary>
    /// Gets or sets the global replicates.
    /// </summary>
    public virtual IList<GlobalReplicate> Replicates { get; set; }
}
