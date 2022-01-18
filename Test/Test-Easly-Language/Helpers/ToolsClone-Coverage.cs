namespace TestEaslyLanguage;

using BaseNode;
using BaseNodeHelper;
using Easly;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public partial class ToolsCloneCoverage
{
    [Test]
    public static void TestDeepCloneNode()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("Foo");

        ClassReplicate SimpleGlobalReplicate = (ClassReplicate)NodeHelper.CreateSimpleClassReplicate("Foo");
        List<ClassReplicate> GlobalReplicateList = new() { SimpleGlobalReplicate };
        IBlock<ClassReplicate> SimpleBlock = BlockListHelper.CreateBlock<ClassReplicate>(GlobalReplicateList);

        SimpleClass.ClassReplicateBlocks.NodeBlockList.Add(SimpleBlock);

        Assert.That(!NodeHelper.IsOptionalAssignedToDefault((IOptionalReference)SimpleClass.FromIdentifier));
            
        SimpleClass.FromIdentifier.Assign();
        Assert.That(NodeHelper.IsOptionalAssignedToDefault((IOptionalReference)SimpleClass.FromIdentifier));

        SimpleClass.FromIdentifier.Clear();

        Class ClassClone;

        ClassClone = (Class)NodeHelper.DeepCloneNode(SimpleClass, false);

        Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("Foo");
        SimpleClass.FromIdentifier.Item = SimpleIdentifier;

        ClassClone = (Class)NodeHelper.DeepCloneNode(SimpleClass, false);

        SimpleClass.FromIdentifier.Unassign();

        ClassClone = (Class)NodeHelper.DeepCloneNode(SimpleClass, false);

        QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("Foo");
        QualifiedName QualifiedNameClone = (QualifiedName)NodeHelper.DeepCloneNode(SimpleQualifiedName, true);

#if !DEBUG
        Class NullClass = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.DeepCloneNode(NullClass, false); });
#endif
    }

    [Test]
    public static void TestDeepCloneNodeList()
    {
        ClassReplicate SimpleGlobalReplicate = NodeHelper.CreateSimpleClassReplicate("Foo");
        List<ClassReplicate> GlobalReplicateList = new() { SimpleGlobalReplicate };

        IList<Node> NodeList = NodeHelper.DeepCloneNodeList(GlobalReplicateList, false);
        Assert.AreEqual(NodeList.Count, GlobalReplicateList.Count);

#if !DEBUG
        List<ClassReplicate> NullList = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.DeepCloneNodeList(NullList, false); });
#endif
    }

    [Test]
    public static void TestDeepCloneBlockList()
    {
        Class SimpleClass = NodeHelper.CreateSimpleClass("Foo");

        ClassReplicate SimpleGlobalReplicate = (ClassReplicate)NodeHelper.CreateSimpleClassReplicate("Foo");
        List<ClassReplicate> GlobalReplicateList = new() { SimpleGlobalReplicate };
        IBlock<ClassReplicate> SimpleBlock = BlockListHelper.CreateBlock<ClassReplicate>(GlobalReplicateList);

        List<IBlock> BlockList = new() { (IBlock)SimpleBlock };

        IList<IBlock> ReplicateBlockList = NodeHelper.DeepCloneBlockList(BlockList, false);

#if !DEBUG
        List<IBlock> NullBlockList = null!;
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.DeepCloneBlockList(NullBlockList, false); });
#endif
    }
}
