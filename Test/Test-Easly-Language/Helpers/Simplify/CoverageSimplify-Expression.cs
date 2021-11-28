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
        public static void TestSimplifyQueryExpression()
        {
            bool Result;
            Node SimplifiedNode;

            QueryExpression Expression1 = NodeHelper.CreateSimpleQueryExpression(String.Empty);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out _);
            Assert.False(Result);

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");
            QueryExpression Expression2 = NodeHelper.CreateQueryExpression(SimpleQualifiedName, new List<Argument>() { NodeHelper.CreateSimplePositionalArgument("b") });

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            QueryExpression Simplified2 = (QueryExpression)SimplifiedNode;
            Assert.That(Simplified2.ArgumentBlocks.NodeBlockList.Count == 0);
        }
    }
}
