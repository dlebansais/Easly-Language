namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        public static void TestDefaultObjects()
        {
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            Assert.That(NodeHelper.IsDefaultNode(DefaultArgument));

            TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
            Assert.That(NodeHelper.IsDefaultNode(DefaultTypeArgument));

            Body DefaultBody = NodeHelper.CreateDefaultBody();
            Assert.That(NodeHelper.IsDefaultNode(DefaultBody));

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            Assert.That(NodeHelper.IsDefaultNode(DefaultExpression));

            Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();
            Assert.That(NodeHelper.IsDefaultNode(DefaultInstruction));

            Feature DefaultFeature = NodeHelper.CreateDefaultFeature();
            Assert.That(NodeHelper.IsDefaultNode(DefaultFeature));

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Assert.That(NodeHelper.IsDefaultNode(DefaultObjectType));
        }

        [Test]
        public static void TestDefaultNode()
        {
            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateDefault(typeof(CoverageSet)); });

            Node DefaultBody = NodeHelper.CreateDefault(typeof(Body));
            Assert.That(NodeHelper.IsDefaultNode(DefaultBody));

            Node DefaultExpression = NodeHelper.CreateDefault(typeof(Expression));
            Assert.That(NodeHelper.IsDefaultNode(DefaultExpression));

            Node DefaultInstruction = NodeHelper.CreateDefault(typeof(Instruction));
            Assert.That(NodeHelper.IsDefaultNode(DefaultInstruction));

            Node DefaultFeature = NodeHelper.CreateDefault(typeof(Feature));
            Assert.That(NodeHelper.IsDefaultNode(DefaultFeature));

            Node DefaultObjectType = NodeHelper.CreateDefault(typeof(ObjectType));
            Assert.That(NodeHelper.IsDefaultNode(DefaultObjectType));

            Node DefaultArgument = NodeHelper.CreateDefault(typeof(Argument));
            Assert.That(NodeHelper.IsDefaultNode(DefaultArgument));

            Node DefaultTypeArgument = NodeHelper.CreateDefault(typeof(TypeArgument));
            Assert.That(NodeHelper.IsDefaultNode(DefaultTypeArgument));

            Node DefaultName = NodeHelper.CreateDefault(typeof(Name));
            Assert.That(NodeHelper.IsDefaultNode(DefaultName));

            Node DefaultIdentifier = NodeHelper.CreateDefault(typeof(Identifier));
            Assert.That(NodeHelper.IsDefaultNode(DefaultIdentifier));

            Node DefaultQualifiedName = NodeHelper.CreateDefault(typeof(QualifiedName));
            Assert.That(NodeHelper.IsDefaultNode(DefaultQualifiedName));

            Node DefaultScope = NodeHelper.CreateDefault(typeof(Scope));
            Assert.That(NodeHelper.IsDefaultNode(DefaultScope));

            Node DefaultImport = NodeHelper.CreateDefault(typeof(Import));
            Assert.That(NodeHelper.IsDefaultNode(DefaultImport));
        }

        [Test]
        public static void TestDefaultNodeType()
        {
            Type DefaultArgumentType = NodeHelper.GetDefaultItemType(typeof(Argument));
            Assert.That(DefaultArgumentType.IsSubclassOf(typeof(Argument)));

            Type DefaultTypeArgumentType = NodeHelper.GetDefaultItemType(typeof(TypeArgument));
            Assert.That(DefaultTypeArgumentType.IsSubclassOf(typeof(TypeArgument)));

            Type DefaultBodyType = NodeHelper.GetDefaultItemType(typeof(Body));
            Assert.That(DefaultBodyType.IsSubclassOf(typeof(Body)));

            Type DefaultExpressionType = NodeHelper.GetDefaultItemType(typeof(Expression));
            Assert.That(DefaultExpressionType.IsSubclassOf(typeof(Expression)));

            Type DefaultInstructionType = NodeHelper.GetDefaultItemType(typeof(Instruction));
            Assert.That(DefaultInstructionType.IsSubclassOf(typeof(Instruction)));

            Type DefaultFeatureType = NodeHelper.GetDefaultItemType(typeof(Feature));
            Assert.That(DefaultFeatureType.IsSubclassOf(typeof(Feature)));

            Type DefaultObjectTypeType = NodeHelper.GetDefaultItemType(typeof(ObjectType));
            Assert.That(DefaultObjectTypeType.IsSubclassOf(typeof(ObjectType)));

            Type DefaultOtherType = NodeHelper.GetDefaultItemType(typeof(CoverageSet));
            Assert.AreEqual(DefaultOtherType, typeof(CoverageSet));
        }

        [Test]
        public static void TestDefaultNodeFromType()
        {
            Node DefaultBody = NodeHelper.CreateDefaultFromType(typeof(Body));
            Node DefaultExpression = NodeHelper.CreateDefaultFromType(typeof(Expression));
            Node DefaultInstruction = NodeHelper.CreateDefaultFromType(typeof(Instruction));
            Node DefaultFeature = NodeHelper.CreateDefaultFromType(typeof(Feature));
            Node DefaultObjectType = NodeHelper.CreateDefaultFromType(typeof(ObjectType));

            Node DefaultArgument = NodeHelper.CreateDefaultFromType(typeof(Argument));
            Node DefaultTypeArgument = NodeHelper.CreateDefaultFromType(typeof(TypeArgument));

            Node DefaultName = NodeHelper.CreateDefaultFromType(typeof(Name));
            Node DefaultIdentifier = NodeHelper.CreateDefaultFromType(typeof(Identifier));
            Node DefaultQualifiedName = NodeHelper.CreateDefaultFromType(typeof(QualifiedName));
            Node DefaultScope = NodeHelper.CreateDefaultFromType(typeof(Scope));
            Node DefaultImport = NodeHelper.CreateDefaultFromType(typeof(Import));

            Node DefaultConstraint = NodeHelper.CreateDefaultFromType(typeof(Constraint));

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateEmptyNode(typeof(CoverageSet)); });
            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateEmptyNode(typeof(Argument)); });

            Class DefaultClass = (Class)NodeHelper.CreateEmptyNode(typeof(Class));
            Assert.That(NodeHelper.IsEmptyNode(DefaultClass));

            Library DefaultLibrary = (Library)NodeHelper.CreateEmptyNode(typeof(Library));
            Assert.That(NodeHelper.IsEmptyNode(DefaultLibrary));

            GlobalReplicate DefaultGlobalReplicate = (GlobalReplicate)NodeHelper.CreateEmptyNode(typeof(GlobalReplicate));
            Assert.That(NodeHelper.IsEmptyNode(DefaultGlobalReplicate));

            InspectInstruction DefaultInspectInstruction = (InspectInstruction)NodeHelper.CreateEmptyNode(typeof(InspectInstruction));
            Assert.That(NodeHelper.IsEmptyNode(DefaultInspectInstruction));

            AttachmentInstruction DefaultAttachmentInstruction = (AttachmentInstruction)NodeHelper.CreateEmptyNode(typeof(AttachmentInstruction));
            Assert.That(NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

            Attachment Attachment0 = DefaultAttachmentInstruction.AttachmentBlocks.NodeBlockList[0].NodeList[0];
            Attachment0.Instructions = NodeHelper.CreateSimpleScope(DefaultInspectInstruction);
            Assert.That(!NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

            Attachment DefaultAttachment1 = (Attachment)NodeHelper.CreateEmptyNode(typeof(Attachment));

            DefaultAttachmentInstruction.AttachmentBlocks.NodeBlockList[0].NodeList.Add(DefaultAttachment1);
            Assert.That(!NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

            Attachment DefaultAttachment2 = (Attachment)NodeHelper.CreateEmptyNode(typeof(Attachment));
            List<Attachment> AttachmentList = new() { DefaultAttachment2 };
            IBlock<Attachment> SimpleBlock = BlockListHelper.CreateBlock<Attachment>(AttachmentList);
            DefaultAttachmentInstruction.AttachmentBlocks.NodeBlockList.Add(SimpleBlock);
            Assert.That(!NodeHelper.IsEmptyNode(DefaultAttachmentInstruction));

            Root DefaultRoot = (Root)NodeHelper.CreateEmptyNode(typeof(Root));
            Assert.That(NodeHelper.IsEmptyNode(DefaultRoot));

            DefaultRoot.Replicates.Add(DefaultGlobalReplicate);
            Assert.That(!NodeHelper.IsEmptyNode(DefaultRoot));

            //System.Diagnostics.Debug.Assert(false);
            Pattern Pattern0 = DefaultGlobalReplicate.Patterns[0];
            Pattern0.Text = "Foo0";
            Assert.That(!NodeHelper.IsEmptyNode(DefaultGlobalReplicate));

            Pattern SimplePattern1 = NodeHelper.CreateSimplePattern("Foo1");
            DefaultGlobalReplicate.Patterns.Add(SimplePattern1);
            Assert.That(!NodeHelper.IsEmptyNode(DefaultGlobalReplicate));
        }

        [Test]
        public static void TestIsNodeType()
        {
            Assert.That(NodeHelper.IsNodeType(typeof(Identifier)));
            Assert.That(!NodeHelper.IsNodeType(typeof(CoverageSet)));
        }
    }
}
