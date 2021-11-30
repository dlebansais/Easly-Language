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

            Result = NodeTreeDiagnostic.IsValid(DefaultExpression, assertValid: true);
            Assert.True(Result);

            Expression InvalidDocExpression = NodeHelper.CreateDefaultExpression();
            InvalidDocExpression.Documentation.Uuid = Guid.Empty;

            Result = NodeTreeDiagnostic.IsValid(InvalidDocExpression, assertValid: false);
            Assert.False(Result);

            GlobalReplicate globalReplicate = NodeHelper.CreateSimpleGlobalReplicate(string.Empty);

            Result = NodeTreeDiagnostic.IsValid(globalReplicate, assertValid: true);
            Assert.True(Result);
        }
    }
}
