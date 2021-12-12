namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeTreeWalkCoverage
    {
        private class TestContext
        {
        }

        [Test]
        public static void TestEnumChildNodeProperties()
        {
            bool Result;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Result = NodeTreeWalk.Walk(DefaultExpression, new WalkCallbacks<TestContext>(), new TestContext());

#if !DEBUG
            Expression NullExpression = null!;
            TestContext NullTestContext = null!;
            Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(NullExpression, new WalkCallbacks<TestContext>(), new TestContext()); });
            Assert.Throws<ArgumentNullException>(() => { NodeTreeWalk.Walk(DefaultExpression, new WalkCallbacks<TestContext>(), NullTestContext); });
#endif
        }
    }
}
