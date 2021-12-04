namespace TestEaslyLanguage
{
    using BaseNode;
    using BaseNodeHelper;
    using Easly;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public partial class NodeTreeDiagnosticCoverage
    {
        [Test]
        public static void Test()
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

            Identifier FirstIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier SecondIdentifier = NodeHelper.CreateSimpleIdentifier("b");

            QualifiedName SimpleQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, SecondIdentifier });
            Result = NodeTreeDiagnostic.IsValid(SimpleQualifiedName, throwOnInvalid: true);
            Assert.True(Result);

            QualifiedName DuplicateQualifiedName = NodeHelper.CreateQualifiedName(new List<Identifier>() { FirstIdentifier, FirstIdentifier });
            Result = NodeTreeDiagnostic.IsValid(DuplicateQualifiedName, throwOnInvalid: false);
            Assert.False(Result);

            Assert.Throws<InvalidNodeException>(() => { NodeTreeDiagnostic.IsValid(DuplicateQualifiedName, throwOnInvalid: true); });
        }
    }
}
