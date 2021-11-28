namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [TestFixture]
    public partial class CoverageSet
    {
        [Test]
        [Category("Simplify")]
        public static void TestSimplifyCommandInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            CommandInstruction Instruction1 = NodeHelper.CreateSimpleCommandInstruction(string.Empty);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out _);
            Assert.False(Result);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            CommandInstruction Instruction2 = NodeHelper.CreateCommandInstruction(SimpleQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("b") });

            Result = NodeHelper.GetSimplifiedInstruction(Instruction2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);

            CommandInstruction Simplified2 = (CommandInstruction)SimplifiedNode;
            Assert.That(Simplified2.ArgumentBlocks.NodeBlockList.Count == 0);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyAsLongAsInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            Continuation EmptyContinuation = NodeHelper.CreateEmptyContinuation();

            AsLongAsInstruction Instruction1 = NodeHelper.CreateAsLongAsInstruction(DefaultExpression, EmptyContinuation);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyAssignmentInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
            List<QualifiedName> SimpleQualifiedNameList = new() { EmptyQualifiedName };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AssignmentInstruction Instruction1 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, DefaultExpression);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyAttachmentInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Name EmptyName = NodeHelper.CreateEmptyName();
            List<Name> SimpleNameList = new() { EmptyName };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AttachmentInstruction Instruction1 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameList);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyCheckInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            CheckInstruction Instruction1 = NodeHelper.CreateCheckInstruction(DefaultExpression);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyCreateInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier EmptyClassIdentifier = NodeHelper.CreateEmptyIdentifier();
            Identifier EmptyRoutineIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Argument> EmptyArgumentList = new();

            CreateInstruction Instruction1 = NodeHelper.CreateCreateInstruction(EmptyClassIdentifier, EmptyRoutineIdentifier, EmptyArgumentList);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyDebugInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();

            DebugInstruction Instruction1 = NodeHelper.CreateSimpleDebugInstruction(DefaultInstruction);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyForLoopInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            ForLoopInstruction Instruction1 = NodeHelper.CreateEmptyForLoopInstruction();

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyIfThenElseInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Conditional EmptyConditional = NodeHelper.CreateEmptyConditional();

            IfThenElseInstruction Instruction1 = NodeHelper.CreateIfThenElseInstruction(EmptyConditional);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyIndexAssignmentInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultArgument);
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            IndexAssignmentInstruction Instruction1 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyKeywordAssignmentInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            KeywordAssignmentInstruction Instruction1 = NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, DefaultExpression);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyOverLoopInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Name EmptyName = NodeHelper.CreateEmptyName();
            List<Name> SimpleNameList = new() { EmptyName };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            OverLoopInstruction Instruction1 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameList);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyPrecursorIndexAssignmentInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultArgument);
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            PrecursorIndexAssignmentInstruction Instruction1 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, SimpleArgumentBlockList, DefaultExpression);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is AssignmentInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyPrecursorInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();

            PrecursorInstruction Instruction1 = NodeHelper.CreatePrecursorInstruction(DefaultObjectType, EmptyArgumentBlockList);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyRaiseEventInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();

            RaiseEventInstruction Instruction1 = NodeHelper.CreateRaiseEventInstruction(EmptyIdentifier);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyReleaseInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();

            ReleaseInstruction Instruction1 = NodeHelper.CreateReleaseInstruction(EmptyQualifiedName);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyThrowInstruction()
        {
            bool Result;
            Node SimplifiedNode;

            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Argument> EmptyArgumentList = new();

            ThrowInstruction Instruction1 = NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, EmptyArgumentList);

            Result = NodeHelper.GetSimplifiedInstruction(Instruction1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is CommandInstruction);
        }
    }
}
