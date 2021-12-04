namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeTreeHelperCoverage
    {
        [Test]
        public static void Test()
        {
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            IList<string> ChildNodePropertyList = NodeTreeHelper.EnumChildNodeProperties(DefaultExpression);
            Assert.True(ChildNodePropertyList.Contains(nameof(Expression.Documentation)));
        }
    }
}
