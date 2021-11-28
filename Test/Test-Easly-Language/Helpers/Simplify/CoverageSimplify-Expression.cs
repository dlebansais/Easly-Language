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
        public static void TestSimplifyAgentExpression()
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
        public static void TestSimplifyAssertionTagExpression()
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
        public static void TestSimplifyBinaryConditionalExpression()
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
        public static void TestSimplifyBinaryOperatorExpression()
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
        public static void TestSimplifyClassConstantExpression()
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
        public static void TestSimplifyCloneOfExpression()
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
        public static void TestSimplifyEntityExpression()
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
        public static void TestSimplifyEqualityExpression()
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
        public static void TestSimplifyIndexQueryExpression()
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
            Assert.That(SimplifiedNode is QueryExpression);
        }

        [Test]
        [Category("Simplify")]
        public static void TestSimplifyInitializedObjectExpression()
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
        public static void TestSimplifyKeywordEntityExpression()
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
        public static void TestSimplifyKeywordExpression()
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
        public static void TestSimplifyManifestCharacterExpression()
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
        public static void TestSimplifyManifestNumberExpression()
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
        public static void TestSimplifyManifestStringExpression()
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
        public static void TestSimplifyNewExpression()
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
        public static void TestSimplifyOldExpression()
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
        public static void TestSimplifyPrecursorExpression()
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
        public static void TestSimplifyPrecursorIndexExpression()
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
        public static void TestSimplifyPreprocessorExpression()
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
        public static void TestSimplifyResultOfExpression()
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
        public static void TestSimplifyUnaryNotExpression()
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
        public static void TestSimplifyUnaryOperatorExpression()
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
