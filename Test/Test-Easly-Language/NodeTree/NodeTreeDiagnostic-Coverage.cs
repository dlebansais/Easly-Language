namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeTreeDiagnosticCoverage
    {
        [Test]
        public static void TestValid()
        {
            bool Result;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Result = NodeTreeDiagnostic.IsValid(DefaultExpression, throwOnInvalid: true);
            Assert.True(Result);

            GlobalReplicate globalReplicate = NodeHelper.CreateSimpleGlobalReplicate(string.Empty);

            Result = NodeTreeDiagnostic.IsValid(globalReplicate, throwOnInvalid: true);
            Assert.True(Result);

            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");

            QualifiedName SimpleQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, SecondIdentifier });
            Result = NodeTreeDiagnostic.IsValid(SimpleQualifiedName, throwOnInvalid: true);
            Assert.True(Result);

            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PrecursorExpression NewPrecursorExpression = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, DefaultObjectType);
            Result = NodeTreeDiagnostic.IsValid(NewPrecursorExpression, throwOnInvalid: true);
            Assert.True(Result);

            NewPrecursorExpression.AncestorType.Clear();
            Result = NodeTreeDiagnostic.IsValid(NewPrecursorExpression, throwOnInvalid: true);
            Assert.True(Result);

            Inheritance NewInheritance = NodeHelper.CreateSimpleInheritance("a");
            Result = NodeTreeDiagnostic.IsValid(NewInheritance, throwOnInvalid: true);
            Assert.True(Result);

            Class NewClass = NodeHelper.CreateSimpleClass("a");
            Result = NodeTreeDiagnostic.IsValid(NewClass, throwOnInvalid: true);
            Assert.True(Result);
        }

        [Test]
        public static void TestInvalidDoc()
        {
            bool Result;

            Expression InvalidDocExpression = NodeHelper.CreateDefaultExpression();
            InvalidDocExpression.Documentation.Uuid = Guid.Empty;

            Result = NodeTreeDiagnostic.IsValid(InvalidDocExpression, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(InvalidDocExpression, throwOnInvalid: true); });
        }

        [Test]
        public static void TestDuplicateNodeInList()
        {
            bool Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            QualifiedName DuplicateQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { SimpleIdentifier, SimpleIdentifier });
            Result = NodeTreeDiagnostic.IsValid(DuplicateQualifiedName, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(DuplicateQualifiedName, throwOnInvalid: true); });
        }

        [Test]
        public static void TestDuplicateNodeInHierarchy()
        {
            bool Result;

            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PrecursorExpression NewPrecursorExpression = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, DefaultObjectType);
            Result = NodeTreeDiagnostic.IsValid(NewPrecursorExpression, throwOnInvalid: true);
            Assert.True(Result);

            PositionalArgument NewArgument = NodeHelper.CreatePositionalArgument(NewPrecursorExpression);
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList<Argument>(NewArgument);

            PrecursorExpression BadPrecursorExpression = NodeHelper.CreatePrecursorExpression(SimpleArgumentBlockList, DefaultObjectType);
            Result = NodeTreeDiagnostic.IsValid(BadPrecursorExpression, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(BadPrecursorExpression, throwOnInvalid: true); });
        }

        [Test]
        public static void TestEmptyBlockList()
        {
            bool Result;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Attachment BadAttachment = NodeHelper.CreateAttachment(DefaultObjectType);
            BadAttachment.AttachTypeBlocks.NodeBlockList.RemoveAt(0);

            Result = NodeTreeDiagnostic.IsValid(BadAttachment, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(BadAttachment, throwOnInvalid: true); });
        }

        [Test]
        public static void TestEmptyNodeList()
        {
            bool Result;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            QualifiedName BadQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { SimpleIdentifier });
            BadQualifiedName.Path.RemoveAt(0);
            Result = NodeTreeDiagnostic.IsValid(BadQualifiedName, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(BadQualifiedName, throwOnInvalid: true); });
        }

        [Test]
        public static void TestDuplicateBlock()
        {
            bool Result;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            PositionalArgument SimplePositionalArgument = NodeHelper.CreateSimplePositionalArgument("b");
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList<Argument>(SimplePositionalArgument);

            QueryExpression BadQueryExpression = NodeHelper.CreateQueryExpression(SimpleQualifiedName, SimpleArgumentBlockList);
            SimpleArgumentBlockList.NodeBlockList.Add(SimpleArgumentBlockList.NodeBlockList[0]);
            Result = NodeTreeDiagnostic.IsValid(BadQueryExpression, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(BadQueryExpression, throwOnInvalid: true); });
        }

        [Test]
        public static void TestDuplicateBlockList()
        {
            bool Result;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            IBlockList<Rename> EmptyRenameBlocks = BlockListHelper.CreateEmptyBlockList<Rename>();
            IBlockList<Identifier> EmptyIdentifierBlocks = BlockListHelper.CreateEmptyBlockList<Identifier>();
            IBlockList<ExportChange> EmptyExportChangeBlocks = BlockListHelper.CreateEmptyBlockList<ExportChange>();

            Inheritance NewInheritance = NodeHelper.CreateInheritance(DefaultObjectType, ConformanceType.Conformant, EmptyRenameBlocks, false, EmptyIdentifierBlocks, false, EmptyIdentifierBlocks, false, EmptyIdentifierBlocks, EmptyExportChangeBlocks);
            Result = NodeTreeDiagnostic.IsValid(NewInheritance, throwOnInvalid: false);
            Assert.False(Result);
            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(NewInheritance, throwOnInvalid: true); });
        }
    }
}
