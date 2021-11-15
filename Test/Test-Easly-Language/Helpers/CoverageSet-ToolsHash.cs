﻿namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        public static void TestHash()
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

            ulong ClassHash;

            ClassHash = NodeHelper.NodeHash(SimpleClass);

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("Foo");
            SimpleClass.FromIdentifier.Item = SimpleIdentifier;

            ClassHash = NodeHelper.NodeHash(SimpleClass);

            SimpleClass.FromIdentifier.Unassign();

            ClassHash = NodeHelper.NodeHash(SimpleClass);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("Foo");
            ulong QualifiedNameHash = NodeHelper.NodeHash(SimpleQualifiedName);
        }
    }
}