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
        public static void TestNodeTreeDiagnostic()
        {
            bool Result;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            Result = NodeTreeDiagnostic.IsValid(DefaultExpression, throwOnInvalid: true);
            Assert.True(Result);

            Expression InvalidDocExpression = NodeHelper.CreateDefaultExpression();
            InvalidDocExpression.Documentation.Uuid = Guid.Empty;

            Result = NodeTreeDiagnostic.IsValid(InvalidDocExpression, throwOnInvalid: false);
            Assert.False(Result);

            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(InvalidDocExpression, throwOnInvalid: true); });

            GlobalReplicate globalReplicate = NodeHelper.CreateSimpleGlobalReplicate(string.Empty);

            Result = NodeTreeDiagnostic.IsValid(globalReplicate, throwOnInvalid: true);
            Assert.True(Result);
        }
    }
}
