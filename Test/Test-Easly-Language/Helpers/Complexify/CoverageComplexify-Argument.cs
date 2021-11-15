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
        [Category("Complexify")]
        public static void TestComplexifyAssignmentArgument()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Identifier> SimpleParameterList = new() { EmptyIdentifier };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AssignmentArgument Argument1 = NodeHelper.CreateAssignmentArgument(SimpleParameterList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Argument1, out ComplexifiedNodeList);
            Assert.False(Result);

            Identifier SplitableIdentifier = NodeHelper.CreateSimpleIdentifier("test,test,test");
            List<Identifier> SplitableParameterList = new() { SplitableIdentifier };

            AssignmentArgument Argument2 = NodeHelper.CreateAssignmentArgument(SplitableParameterList, DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Argument2, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentArgument);

            AssignmentArgument Complexified2 = (AssignmentArgument)ComplexifiedNodeList[0];
            Assert.That(Complexified2.ParameterBlocks.NodeBlockList.Count == 1);
            Assert.That(Complexified2.ParameterBlocks.NodeBlockList[0].NodeList.Count == 3);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            AssignmentArgument Argument3 = NodeHelper.CreateAssignmentArgument(SimpleParameterList, NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Argument3, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentArgument);

            AssignmentArgument Complexified3 = (AssignmentArgument)ComplexifiedNodeList[0];
            Assert.That(Complexified3.Source is ManifestNumberExpression);
        }

        [Test]
        [Category("Complexify")]
        public static void TestComplexifyPositionalArgument()
        {
            bool Result;
            IList<Node> ComplexifiedNodeList;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            PositionalArgument Argument4 = NodeHelper.CreatePositionalArgument(DefaultExpression);

            Result = NodeHelper.GetComplexifiedNode(Argument4, out ComplexifiedNodeList);
            Assert.False(Result);

            Expression NumberExpression = NodeHelper.CreateSimpleQueryExpression("0");

            PositionalArgument Argument5 = NodeHelper.CreatePositionalArgument(NumberExpression);

            Result = NodeHelper.GetComplexifiedNode(Argument5, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is PositionalArgument);

            Expression AssignmentExpression = NodeHelper.CreateSimpleQueryExpression("x:=y");

            PositionalArgument Argument6 = NodeHelper.CreatePositionalArgument(AssignmentExpression);

            Result = NodeHelper.GetComplexifiedNode(Argument6, out ComplexifiedNodeList);
            Assert.True(Result);
            Assert.AreEqual(ComplexifiedNodeList.Count, 1);
            Assert.That(ComplexifiedNodeList[0] is AssignmentArgument);
        }
    }
}
