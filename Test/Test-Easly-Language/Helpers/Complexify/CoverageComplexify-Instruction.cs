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

            System.Diagnostics.Debugger.Launch();
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
    }
}
