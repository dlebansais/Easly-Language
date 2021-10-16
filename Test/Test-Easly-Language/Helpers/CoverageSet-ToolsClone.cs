﻿namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        public static void TestClone()
        {
            Class SimpleClass = NodeHelper.CreateSimpleClass("Foo");

            ClassReplicate SimpleGlobalReplicate = (ClassReplicate)NodeHelper.CreateSimpleClassReplicate("Foo");
            List<ClassReplicate> GlobalReplicateList = new() { SimpleGlobalReplicate };
            IBlock<ClassReplicate> SimpleBlock = BlockListHelper.CreateBlock<ClassReplicate>(GlobalReplicateList);

            SimpleClass.ClassReplicateBlocks.NodeBlockList.Add(SimpleBlock);

            Class ClassClone = (Class)NodeHelper.DeepCloneNode(SimpleClass, false);

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("Foo");
            SimpleClass.FromIdentifier.Item = SimpleIdentifier;

            ClassClone = (Class)NodeHelper.DeepCloneNode(SimpleClass, false);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("Foo");
            QualifiedName QualifiedNameClone = (QualifiedName)NodeHelper.DeepCloneNode(SimpleQualifiedName, true);
        }
    }
}
