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
        public static void TestSimplifyAssignmentArgument()
        {
            bool Result;
            Node SimplifiedNode;

            //System.Diagnostics.Debugger.Launch();
            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Identifier> SimpleParameterList = new() { EmptyIdentifier };
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            AssignmentArgument Argument1 = NodeHelper.CreateAssignmentArgument(SimpleParameterList, DefaultExpression);

            Result = NodeHelper.GetSimplifiedArgument(Argument1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is PositionalArgument);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyPositionalArgument()
        {
            bool Result;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            PositionalArgument Argument1 = NodeHelper.CreatePositionalArgument(DefaultExpression);

            Result = NodeHelper.GetSimplifiedArgument(Argument1, out _);
            Assert.False(Result);
        }
    }
}
