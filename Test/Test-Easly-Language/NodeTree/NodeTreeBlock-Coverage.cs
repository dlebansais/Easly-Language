namespace TestEaslyLanguage;

#if !DEBUG
using System;
#endif
using System.Collections.Generic;
using BaseNode;
using BaseNodeHelper;
using NUnit.Framework;

[TestFixture]
public partial class NodeTreeBlockCoverage
{
    [Test]
    public static void Test()
    {
        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");
        IBlock SimpleIdentifierBlock = (IBlock)BlockListHelper.CreateBlock(new List<Identifier>() { SimpleIdentifier });

        Library SimpleLibrary = NodeHelper.CreateSimpleLibrary("a");
        NodeTreeHelperBlockList.InsertIntoBlockList(SimpleLibrary, nameof(Library.ClassIdentifierBlocks), 0, SimpleIdentifierBlock);

        NodeTreeHelperBlockList.GetChildBlockList(SimpleLibrary, nameof(Library.ClassIdentifierBlocks), out IList<NodeTreeBlock> ChildBlockList);
        Assert.AreEqual(ChildBlockList.Count, 1);

        NodeTreeBlock FirstNodeTreeBlock = ChildBlockList[0];
        Assert.AreEqual(FirstNodeTreeBlock.ReplicationPattern.Text, "*");
        Assert.AreEqual(FirstNodeTreeBlock.SourceIdentifier.Text, string.Empty);

        IReadOnlyList<Node> NodeList = FirstNodeTreeBlock.NodeList;
        Assert.AreEqual(NodeList.Count, 1);
        Assert.AreEqual(NodeList[0], SimpleIdentifier);

#if !DEBUG
        Library NullLibrary = null!;
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlockList(NullLibrary, nameof(Library.ClassIdentifierBlocks), out _); });
        Assert.Throws<ArgumentNullException>(() => { NodeTreeHelperBlockList.GetChildBlockList(SimpleLibrary, NullString, out _); });
#endif
    }
}
