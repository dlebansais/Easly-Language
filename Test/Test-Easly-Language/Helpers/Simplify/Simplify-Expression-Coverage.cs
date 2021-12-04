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
    public partial class SimplifyExpressionCoverage
    {
        [Test]
        [Category("Simplify")]
        public static void TestQueryExpression()
        {
            bool Result;
            Node SimplifiedNode;

            QueryExpression Expression1 = NodeHelper.CreateSimpleQueryExpression(string.Empty);

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

        [Test]
        [Category("Simplify")]
        public static void TestAgentExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
            AgentExpression Expression1 = NodeHelper.CreateAgentExpression(SimpleIdentifier, DefaultObjectType);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestAssertionTagExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            AssertionTagExpression Expression1 = NodeHelper.CreateAssertionTagExpression(SimpleIdentifier);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestBinaryConditionalExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            BinaryConditionalExpression Expression1 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.And, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            BinaryConditionalExpression Expression2 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.Or, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            BinaryConditionalExpression Expression3 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.Xor, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression3, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            BinaryConditionalExpression Expression4 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.Implies, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression4, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression LeftNumberExpression = NodeHelper.CreateDefaultManifestNumberExpression();
            Expression RightNumberExpression = NodeHelper.CreateDefaultManifestNumberExpression();

            BinaryConditionalExpression Expression5 = NodeHelper.CreateBinaryConditionalExpression(LeftNumberExpression, ConditionalTypes.And, RightNumberExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression5, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression LeftStringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);
            Expression RightStringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            BinaryConditionalExpression Expression6 = NodeHelper.CreateBinaryConditionalExpression(LeftStringExpression, ConditionalTypes.And, RightStringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression6, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Simplify")]
        public static void TestBinaryOperatorExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();
            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("+");

            BinaryOperatorExpression Expression1 = NodeHelper.CreateBinaryOperatorExpression(LeftExpression, SimpleIdentifier, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression LeftStringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);
            Expression RightStringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            BinaryOperatorExpression Expression2 = NodeHelper.CreateBinaryOperatorExpression(LeftStringExpression, SimpleIdentifier, RightStringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Simplify")]
        public static void TestClassConstantExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier SimpleClassIdentifier = NodeHelper.CreateSimpleIdentifier("a");
            Identifier SimpleConstantIdentifier = NodeHelper.CreateSimpleIdentifier("a");

            ClassConstantExpression Expression1 = NodeHelper.CreateClassConstantExpression(SimpleClassIdentifier, SimpleConstantIdentifier);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestCloneOfExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            CloneOfExpression Expression1 = NodeHelper.CreateCloneOfExpression(CloneType.Deep, DefaultExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression StringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            CloneOfExpression Expression2 = NodeHelper.CreateCloneOfExpression(CloneType.Deep, StringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Simplify")]
        public static void TestEntityExpression()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            EntityExpression Expression1 = NodeHelper.CreateEntityExpression(SimpleQualifiedName);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestEqualityExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            EqualityExpression Expression1 = NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Equal, EqualityType.Physical, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            EqualityExpression Expression2 = NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Different, EqualityType.Physical, RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression LeftStringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);
            Expression RightStringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            EqualityExpression Expression3 = NodeHelper.CreateEqualityExpression(LeftStringExpression, ComparisonType.Equal, EqualityType.Physical, RightStringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression3, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Simplify")]
        public static void TestIndexQueryExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
            Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
            List<Argument> DefaultArgumentList = new() { DefaultArgument };

            IndexQueryExpression Expression1 = NodeHelper.CreateIndexQueryExpression(DefaultExpression, DefaultArgumentList);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            ManifestNumberExpression NumberExpression = NodeHelper.CreateDefaultManifestNumberExpression();

            IndexQueryExpression Expression2 = NodeHelper.CreateIndexQueryExpression(NumberExpression, DefaultArgumentList);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is ManifestNumberExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestInitializedObjectExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("+");
            List<AssignmentArgument> EmptyAssignmentArgumentList = new();

            InitializedObjectExpression Expression1 = NodeHelper.CreateInitializedObjectExpression(SimpleIdentifier, EmptyAssignmentArgumentList);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            AssignmentArgument SimpleAssignmentArgument = NodeHelper.CreateSimpleAssignmentArgument("a", "b");
            List<AssignmentArgument> SimpleAssignmentArgumentList = new() { SimpleAssignmentArgument };

            InitializedObjectExpression Expression2 = NodeHelper.CreateInitializedObjectExpression(SimpleIdentifier, SimpleAssignmentArgumentList);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestKeywordEntityExpression()
        {
            bool Result;
            Node SimplifiedNode;

            KeywordEntityExpression Expression1 = NodeHelper.CreateKeywordEntityExpression(Keyword.Current);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestKeywordExpression()
        {
            bool Result;
            Node SimplifiedNode;

            KeywordExpression Expression1 = NodeHelper.CreateKeywordExpression(Keyword.Current);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestManifestCharacterExpression()
        {
            bool Result;
            Node SimplifiedNode;

            ManifestCharacterExpression Expression1 = NodeHelper.CreateManifestCharacterExpression("a");

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestManifestNumberExpression()
        {
            bool Result;
            Node SimplifiedNode;

            ManifestNumberExpression Expression1 = NodeHelper.CreateDefaultManifestNumberExpression();

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestManifestStringExpression()
        {
            bool Result;
            Node SimplifiedNode;

            ManifestStringExpression Expression1 = NodeHelper.CreateManifestStringExpression(string.Empty);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestNewExpression()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            NewExpression Expression1 = NodeHelper.CreateNewExpression(SimpleQualifiedName);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestOldExpression()
        {
            bool Result;
            Node SimplifiedNode;

            QualifiedName SimpleQualifiedName = NodeHelper.CreateSimpleQualifiedName("a");

            OldExpression Expression1 = NodeHelper.CreateOldExpression(SimpleQualifiedName);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestPrecursorExpression()
        {
            bool Result;
            Node SimplifiedNode;

            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PrecursorExpression Expression1 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, DefaultObjectType);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestPrecursorIndexExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Argument SimpleArgument = NodeHelper.CreateSimplePositionalArgument("a");
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(SimpleArgument);
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            PrecursorIndexExpression Expression1 = NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentBlockList, DefaultObjectType);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestPreprocessorExpression()
        {
            bool Result;
            Node SimplifiedNode;

            PreprocessorExpression Expression1 = NodeHelper.CreatePreprocessorExpression(PreprocessorMacro.DateAndTime);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestResultOfExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            ResultOfExpression Expression1 = NodeHelper.CreateResultOfExpression(DefaultExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression StringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            ResultOfExpression Expression2 = NodeHelper.CreateResultOfExpression(StringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Simplify")]
        public static void TestUnaryNotExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            UnaryNotExpression Expression1 = NodeHelper.CreateUnaryNotExpression(RightExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression StringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            UnaryNotExpression Expression2 = NodeHelper.CreateUnaryNotExpression(StringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out _);
            Assert.False(Result);
        }

        [Test]
        [Category("Simplify")]
        public static void TestUnaryOperatorExpression()
        {
            bool Result;
            Node SimplifiedNode;

            Identifier SimpleIdentifier = NodeHelper.CreateSimpleIdentifier("-");
            Expression DefaultExpression = NodeHelper.CreateDefaultExpression();

            UnaryOperatorExpression Expression1 = NodeHelper.CreateUnaryOperatorExpression(SimpleIdentifier, DefaultExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression1, out SimplifiedNode);
            Assert.True(Result);
            Assert.That(SimplifiedNode is QueryExpression);

            Expression StringExpression = NodeHelper.CreateManifestStringExpression(string.Empty);

            UnaryOperatorExpression Expression2 = NodeHelper.CreateUnaryOperatorExpression(SimpleIdentifier, StringExpression);

            Result = NodeHelper.GetSimplifiedExpression(Expression2, out _);
            Assert.False(Result);
        }
    }
}
