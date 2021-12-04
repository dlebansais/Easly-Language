namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public partial class ToolsCloneCoverage
    {
        [Test]
        public static void Test()
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
        }
    }
}
