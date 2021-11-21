namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        [Category("Complexify")]
        public static void TestComplexifyCommandInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            CommandInstruction Instruction1 = NodeHelper.CreateSimpleCommandInstruction(String.Empty);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            CommandInstruction Instruction2 = NodeHelper.CreateSimpleCommandInstruction("a.b");

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CommandInstruction);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            CommandInstruction Instruction3 = NodeHelper.CreateCommandInstruction(SimpleQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("a,b")});

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CommandInstruction);

            CommandInstruction Complexified3 = (CommandInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified3.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified3.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 2);

            CommandInstruction Instruction4 = NodeHelper.CreateCommandInstruction(NodeHelper.CreateSimpleQualifiedName("a(b,c)"), new List<Argument>());

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 2);
            Assert.That(ComplexifiedNodeList[0] is CommandInstruction);
            Assert.That(ComplexifiedNodeList[1] is CommandInstruction);

            CommandInstruction Complexified4_0 = (CommandInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified4_0.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified4_0.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 1);

            CommandInstruction Complexified4_1 = (CommandInstruction)ComplexifiedNodeList[1];
            Assert.That(Complexified4_1.ArgumentBlocks.NodeBlockList.Count == 1 && Complexified4_1.ArgumentBlocks.NodeBlockList[0].NodeList.Count == 2);

            CommandInstruction Instruction5 = NodeHelper.CreateSimpleCommandInstruction("as long as a");

            Result = NodeHelper.GetComplexifiedNode(Instruction5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AsLongAsInstruction);

            CommandInstruction Instruction6 = NodeHelper.CreateSimpleCommandInstruction("Result:=b");

            Result = NodeHelper.GetComplexifiedNode(Instruction6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 2);
            Assert.That(ComplexifiedNodeList[0] is AssignmentInstruction);
            Assert.That(ComplexifiedNodeList[1] is KeywordAssignmentInstruction);

            KeywordAssignmentInstruction Complexified6 = (KeywordAssignmentInstruction)ComplexifiedNodeList[1];
            Assert.AreEqual(Complexified6.Destination, Keyword.Result);

            CommandInstruction Instruction7 = NodeHelper.CreateSimpleCommandInstruction("attach a");

            Result = NodeHelper.GetComplexifiedNode(Instruction7, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AttachmentInstruction);

            CommandInstruction Instruction8 = NodeHelper.CreateSimpleCommandInstruction("check a");

            Result = NodeHelper.GetComplexifiedNode(Instruction8, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CheckInstruction);

            CommandInstruction Instruction9 = NodeHelper.CreateSimpleCommandInstruction("create a");

            Result = NodeHelper.GetComplexifiedNode(Instruction9, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CreateInstruction);

            CommandInstruction Instruction10 = NodeHelper.CreateSimpleCommandInstruction("debug a");

            Result = NodeHelper.GetComplexifiedNode(Instruction10, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is DebugInstruction);

            CommandInstruction Instruction11 = NodeHelper.CreateSimpleCommandInstruction("for a");

            Result = NodeHelper.GetComplexifiedNode(Instruction11, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ForLoopInstruction);

            CommandInstruction Instruction12 = NodeHelper.CreateSimpleCommandInstruction("if a");

            Result = NodeHelper.GetComplexifiedNode(Instruction12, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IfThenElseInstruction);

            QualifiedName IndexQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { NodeHelper.CreateSimpleIdentifier("a"), NodeHelper.CreateSimpleIdentifier("[]:=") });
            CommandInstruction Instruction13 = NodeHelper.CreateCommandInstruction(IndexQualifiedName, new List<Argument>());

            Result = NodeHelper.GetComplexifiedNode(Instruction13, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexAssignmentInstruction);

            CommandInstruction Instruction14 = NodeHelper.CreateSimpleCommandInstruction("inspect a");

            Result = NodeHelper.GetComplexifiedNode(Instruction14, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is InspectInstruction);

            CommandInstruction Instruction15 = NodeHelper.CreateSimpleCommandInstruction("over a");

            Result = NodeHelper.GetComplexifiedNode(Instruction15, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is OverLoopInstruction);

            QualifiedName PrecursorIndexQualifiedName = NodeHelper.CreateSimpleQualifiedName("precursor[]:=");
            CommandInstruction Instruction16 = NodeHelper.CreateCommandInstruction(PrecursorIndexQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("a") });

            Result = NodeHelper.GetComplexifiedNode(Instruction16, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexAssignmentInstruction);

            CommandInstruction Instruction17 = NodeHelper.CreateSimpleCommandInstruction("precursor");

            Result = NodeHelper.GetComplexifiedNode(Instruction17, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorInstruction);

            CommandInstruction Instruction18 = NodeHelper.CreateSimpleCommandInstruction("raise a");

            Result = NodeHelper.GetComplexifiedNode(Instruction18, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is RaiseEventInstruction);

            CommandInstruction Instruction19 = NodeHelper.CreateSimpleCommandInstruction("release a");

            Result = NodeHelper.GetComplexifiedNode(Instruction19, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ReleaseInstruction);

            CommandInstruction Instruction20 = NodeHelper.CreateSimpleCommandInstruction("throw a");

            Result = NodeHelper.GetComplexifiedNode(Instruction20, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ThrowInstruction);

            Identifier StartIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier MiddleIdentifier = NodeHelper.CreateSimpleIdentifier("b:=c");
            Identifier EndIdentifier = NodeHelper.CreateSimpleIdentifier("d");
            List<Identifier> AssignmentPathList = new() { StartIdentifier , MiddleIdentifier, EndIdentifier };
            QualifiedName AssignmentPathQualifiedName = NodeHelper.CreateQualifiedName(AssignmentPathList);
            CommandInstruction Instruction21 = NodeHelper.CreateCommandInstruction(AssignmentPathQualifiedName, new List<Argument>());

            Result = NodeHelper.GetComplexifiedNode(Instruction21, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentInstruction);

            CommandInstruction Instruction22 = NodeHelper.CreateSimpleCommandInstruction("attach a to b");

            Result = NodeHelper.GetComplexifiedNode(Instruction22, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AttachmentInstruction);

            CommandInstruction Instruction23 = NodeHelper.CreateCommandInstruction(IndexQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("a") });

            Result = NodeHelper.GetComplexifiedNode(Instruction23, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexAssignmentInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyAsLongAsInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            Continuation EmptyContinuation = NodeHelper.CreateEmptyContinuation();

            AsLongAsInstruction Instruction1 = NodeHelper.CreateAsLongAsInstruction(DefaultExpression, EmptyContinuation);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            AsLongAsInstruction Instruction2 = NodeHelper.CreateAsLongAsInstruction(NumberExpression, EmptyContinuation);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AsLongAsInstruction);

            AsLongAsInstruction Complexified2 = (AsLongAsInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified2.ContinueCondition is ManifestNumberExpression);

            IBlockList<Continuation> SimpleContinuationBlockList = BlockListHelper.CreateSimpleBlockList(EmptyContinuation);
            Scope EmptyScope = NodeHelper.CreateEmptyScope();

            AsLongAsInstruction Instruction3 = NodeHelper.CreateAsLongAsInstruction(NumberExpression, SimpleContinuationBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AsLongAsInstruction);

            AsLongAsInstruction Complexified3 = (AsLongAsInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified3.ContinueCondition is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyAssignmentInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
            List<QualifiedName> SimpleQualifiedNameList = new() { EmptyQualifiedName };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AssignmentInstruction Instruction1 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            QualifiedName SplittableQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            AssignmentInstruction Instruction2 = NodeHelper.CreateAssignmentInstruction(new List<QualifiedName>() { SplittableQualifiedName }, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentInstruction);

            AssignmentInstruction Complexified2 = (AssignmentInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified2.DestinationBlocks.NodeBlockList.Count == 1 && Complexified2.DestinationBlocks.NodeBlockList[0].NodeList.Count == 1);
            QualifiedName ComplexifiedQualifiedName = Complexified2.DestinationBlocks.NodeBlockList[0].NodeList[0];
            Assert.That(ComplexifiedQualifiedName.Path.Count == 2);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            AssignmentInstruction Instruction3 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentInstruction);

            AssignmentInstruction Complexified3 = (AssignmentInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified3.Source is ManifestNumberExpression);

            QualifiedName ResultQualifiedName = NodeHelper.CreateSimpleQualifiedName("Result");

            AssignmentInstruction Instruction4 = NodeHelper.CreateAssignmentInstruction(new List<QualifiedName>() { ResultQualifiedName }, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is KeywordAssignmentInstruction);

            KeywordAssignmentInstruction Complexified4 = (KeywordAssignmentInstruction)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified4.Destination, Keyword.Result);

            QualifiedName RetryQualifiedName = NodeHelper.CreateSimpleQualifiedName("Retry");

            AssignmentInstruction Instruction5 = NodeHelper.CreateAssignmentInstruction(new List<QualifiedName>() { RetryQualifiedName }, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is KeywordAssignmentInstruction);

            KeywordAssignmentInstruction Complexified5 = (KeywordAssignmentInstruction)ComplexifiedNodeList[0];
            Assert.AreEqual(Complexified5.Destination, Keyword.Retry);

            QualifiedName IndexQualifiedName = NodeHelper.CreateSimpleQualifiedName("a[b]");

            AssignmentInstruction Instruction6 = NodeHelper.CreateAssignmentInstruction(new List<QualifiedName>() { IndexQualifiedName }, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexAssignmentInstruction);

            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            QualifiedName FirstQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier });
            IBlock<QualifiedName> FirstBlock = BlockListHelper.CreateBlock(new List<QualifiedName>() { FirstQualifiedName });
            Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");
            QualifiedName SecondQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { SecondIdentifier });
            IBlock<QualifiedName> SecondBlock = BlockListHelper.CreateBlock(new List<QualifiedName>() { SecondQualifiedName });
            IBlockList<QualifiedName> NotSimpleBlockList = BlockListHelper.CreateBlockList(new List<IBlock<QualifiedName>>() { FirstBlock, SecondBlock});

            AssignmentInstruction Instruction7 = NodeHelper.CreateAssignmentInstruction(NotSimpleBlockList, DefaultExpression);

            //System.Diagnostics.Debugger.Launch();
            Result = NodeHelper.GetComplexifiedNode(Instruction7, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyAttachmentInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Name EmptyName = NodeHelper.CreateEmptyName();
            List<Name> SimpleNameList = new() { EmptyName };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AttachmentInstruction Instruction1 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameList);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            AttachmentInstruction Instruction2 = NodeHelper.CreateAttachmentInstruction(NumberExpression, SimpleNameList);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AttachmentInstruction);

            IBlockList<Name> SimpleEntityNameBlockList = BlockListHelper.CreateSimpleBlockList(EmptyName);
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Attachment EmptyAttachment = NodeHelper.CreateAttachment(DefaultObjectType);
            IBlockList<Attachment> SimpleAttachmentBlockList = BlockListHelper.CreateSimpleBlockList(EmptyAttachment);

            Scope EmptyScope = NodeHelper.CreateEmptyScope();
            AttachmentInstruction Instruction3 = NodeHelper.CreateAttachmentInstruction(NumberExpression, SimpleEntityNameBlockList, SimpleAttachmentBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AttachmentInstruction);

            Name SplittableName = NodeHelper.CreateSimpleName("a,b");

            AttachmentInstruction Instruction4 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, new List<Name>() { SplittableName });

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AttachmentInstruction);

            IBlockList<Name> SplittableEntityNameBlockList = BlockListHelper.CreateSimpleBlockList(SplittableName);

            AttachmentInstruction Instruction5 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SplittableEntityNameBlockList, SimpleAttachmentBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Instruction5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AttachmentInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyCheckInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            CheckInstruction Instruction1 = NodeHelper.CreateCheckInstruction(DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            CheckInstruction Instruction2 = NodeHelper.CreateCheckInstruction(NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CheckInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyCreateInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Identifier EmptyClassIdentifier = NodeHelper.CreateEmptyIdentifier();
            Identifier EmptyRoutineIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Argument> EmptyArgumentList = new();
            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();

            CreateInstruction Instruction1 = NodeHelper.CreateCreateInstruction(EmptyClassIdentifier, EmptyRoutineIdentifier, EmptyArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Argument SplittableArgument = NodeHelper.CreateSimplePositionalArgument("a,b");

            CreateInstruction Instruction2 = NodeHelper.CreateCreateInstruction(EmptyClassIdentifier, EmptyRoutineIdentifier, new List<Argument>() { SplittableArgument });

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CreateInstruction);

            IBlockList<Argument> SplittableArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SplittableArgument);

            CreateInstruction Instruction3 = NodeHelper.CreateCreateInstruction(EmptyClassIdentifier, EmptyRoutineIdentifier, SplittableArgumentBlockList, EmptyQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CreateInstruction);

            QualifiedName SplittableQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            CreateInstruction Instruction4 = NodeHelper.CreateCreateInstruction(EmptyClassIdentifier, EmptyRoutineIdentifier, EmptyArgumentBlockList, SplittableQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is CreateInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyIfThenElseInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Conditional EmptyConditional = NodeHelper.CreateEmptyConditional();

            IfThenElseInstruction Instruction1 = NodeHelper.CreateIfThenElseInstruction(EmptyConditional);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");
            Conditional NumberConditional = NodeHelper.CreateConditional(NumberExpression);

            IfThenElseInstruction Instruction2 = NodeHelper.CreateIfThenElseInstruction(NumberConditional);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IfThenElseInstruction);

            IBlockList<Conditional> NumberConditionalBlockList = BlockListHelper.CreateSimpleBlockList(NumberConditional);
            Scope EmptyScope = NodeHelper.CreateEmptyScope();
            IfThenElseInstruction Instruction3 = NodeHelper.CreateIfThenElseInstruction(NumberConditionalBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IfThenElseInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyIndexAssignmentInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultArgument);
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            IndexAssignmentInstruction Instruction1 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            QualifiedName SplittableQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            IndexAssignmentInstruction Instruction2 = NodeHelper.CreateIndexAssignmentInstruction(SplittableQualifiedName, SimpleArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexAssignmentInstruction);

            Argument SplittableArgument = NodeHelper.CreateSimplePositionalArgument("a,b");
            IBlockList<Argument> SplittableArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SplittableArgument);

            IndexAssignmentInstruction Instruction3 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SplittableArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexAssignmentInstruction);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            IndexAssignmentInstruction Instruction4 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentBlockList, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is IndexAssignmentInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyInspectInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            InspectInstruction Instruction1 = NodeHelper.CreateInspectInstruction(DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            InspectInstruction Instruction2 = NodeHelper.CreateInspectInstruction(NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is InspectInstruction);

            With SimpleWith = NodeHelper.CreateSimpleWith(DefaultExpression);
            IBlockList<With> SimpleWithBlockList = BlockListHelper.CreateSimpleBlockList(SimpleWith);
            Scope EmptyScope = NodeHelper.CreateEmptyScope();
            InspectInstruction Instruction3 = NodeHelper.CreateInspectInstruction(NumberExpression, SimpleWithBlockList, EmptyScope);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is InspectInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyKeywordAssignmentInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            KeywordAssignmentInstruction Instruction1 = NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            KeywordAssignmentInstruction Instruction2 = NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is KeywordAssignmentInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyOverLoopInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Name EmptyName = NodeHelper.CreateEmptyName();
            List<Name> SimpleNameList = new() { EmptyName };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            OverLoopInstruction Instruction1 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameList);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            OverLoopInstruction Instruction2 = NodeHelper.CreateOverLoopInstruction(NumberExpression, SimpleNameList);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is OverLoopInstruction);

            IBlockList<Name> SimpleNameBlockList = BlockListHelper.CreateSimpleBlockList(EmptyName);
            Scope EmptyScope = NodeHelper.CreateEmptyScope();
            IBlockList<Assertion> EmptyAssertionBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();

            OverLoopInstruction Instruction3 = NodeHelper.CreateOverLoopInstruction(NumberExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, EmptyIdentifier, EmptyAssertionBlockList);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is OverLoopInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyPrecursorIndexAssignmentInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultArgument);
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            PrecursorIndexAssignmentInstruction Instruction1 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, SimpleArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like b");

            PrecursorIndexAssignmentInstruction Instruction2 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(AnchorType, SimpleArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexAssignmentInstruction);

            PrecursorIndexAssignmentInstruction Complexified2 = (PrecursorIndexAssignmentInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified2.AncestorType.IsAssigned);
            Assert.That(Complexified2.AncestorType.Item is AnchoredType);

            Argument SplittableArgument = NodeHelper.CreateSimplePositionalArgument("a,b");
            IBlockList<Argument> SplittableArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SplittableArgument);

            PrecursorIndexAssignmentInstruction Instruction3 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(SplittableArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexAssignmentInstruction);

            PrecursorIndexAssignmentInstruction Instruction4 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, SplittableArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexAssignmentInstruction);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            PrecursorIndexAssignmentInstruction Instruction5 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(SimpleArgumentBlockList, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexAssignmentInstruction);

            PrecursorIndexAssignmentInstruction Instruction6 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, SimpleArgumentBlockList, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Instruction6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorIndexAssignmentInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyPrecursorInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();

            PrecursorInstruction Instruction1 = NodeHelper.CreatePrecursorInstruction(DefaultObjectType, EmptyArgumentBlockList);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like b");

            PrecursorInstruction Instruction2 = NodeHelper.CreatePrecursorInstruction(AnchorType, EmptyArgumentBlockList);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorInstruction);

            PrecursorInstruction Complexified2 = (PrecursorInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified2.AncestorType.IsAssigned);
            Assert.That(Complexified2.AncestorType.Item is AnchoredType);

            Argument SplittableArgument = NodeHelper.CreateSimplePositionalArgument("a,b");
            IBlockList<Argument> SplittableArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SplittableArgument);

            PrecursorInstruction Instruction3 = NodeHelper.CreatePrecursorInstruction(SplittableArgumentBlockList);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorInstruction);

            PrecursorInstruction Instruction4 = NodeHelper.CreatePrecursorInstruction(DefaultObjectType, SplittableArgumentBlockList);

            Result = NodeHelper.GetComplexifiedNode(Instruction4, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PrecursorInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyReleaseInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();

            ReleaseInstruction Instruction1 = NodeHelper.CreateReleaseInstruction(EmptyQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            QualifiedName SplittableQualifiedName = NodeHelper.CreateSimpleQualifiedName("a.b");

            ReleaseInstruction Instruction2 = NodeHelper.CreateReleaseInstruction(SplittableQualifiedName);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ReleaseInstruction);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyThrowInstruction()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Argument> EmptyArgumentList = new();
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();

            ThrowInstruction Instruction1 = NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, EmptyArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Instruction1, out _);
            Assert.False(Result);

            ObjectType AnchorType = NodeHelper.CreateSimpleSimpleType("like b");

            ThrowInstruction Instruction2 = NodeHelper.CreateThrowInstruction(AnchorType, EmptyIdentifier, EmptyArgumentList);

            Result = NodeHelper.GetComplexifiedNode(Instruction2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ThrowInstruction);

            ThrowInstruction Complexified2 = (ThrowInstruction)ComplexifiedNodeList[0];
            Assert.That(Complexified2.ExceptionType is AnchoredType);

            Argument SplittableArgument = NodeHelper.CreateSimplePositionalArgument("a,b");
            IBlockList<Argument> SplittableArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SplittableArgument);

            ThrowInstruction Instruction3 = NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, SplittableArgumentBlockList);

            Result = NodeHelper.GetComplexifiedNode(Instruction3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is ThrowInstruction);
        }
    }
}
