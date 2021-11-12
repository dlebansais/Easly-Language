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
        public static void TestSpecificObjectsArgument()
        {
            List<Identifier> EmptyIdentifierList = new();

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Identifier> IdentifierList = new() { EmptyIdentifier };

            Expression Source = NodeHelper.CreateDefaultExpression();

            AssignmentArgument Argument1 = NodeHelper.CreateAssignmentArgument(IdentifierList, Source);
            Assert.Equals(Argument1.Source, Source);

            NodeHelper.CreateAssignmentArgument(IdentifierList, null!);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateAssignmentArgument(EmptyIdentifierList, Source); });

            IBlockList<Identifier> EmptyBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
            IBlockList<Identifier> SimpleBlockList = BlockListHelper.CreateSimpleBlockList(EmptyIdentifier);

            AssignmentArgument Argument2 = NodeHelper.CreateAssignmentArgument(SimpleBlockList, Source);
            Assert.Equals(Argument2.ParameterBlocks, SimpleBlockList);
            Assert.Equals(Argument2.Source, Source);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateAssignmentArgument(EmptyBlockList, Source); });

            PositionalArgument Argument3 = NodeHelper.CreatePositionalArgument(Source);
            Assert.Equals(Argument3.Source, Source);
        }

        [Test]
        public static void TestSpecificObjectsBody()
        {
            DeferredBody Body1 = NodeHelper.CreateEmptyDeferredBody();
            EffectiveBody Body2 = NodeHelper.CreateEmptyEffectiveBody();
            ExternBody Body3 = NodeHelper.CreateEmptyExternBody();
            PrecursorBody Body4 = NodeHelper.CreateEmptyPrecursorBody();

            Document Documentation = NodeHelper.CreateEmptyDocumentation();
            IBlockList<Assertion> EmptyRequireBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
            IBlockList<Assertion> EmptyEnsureBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
            IBlockList<Identifier> EmptyExceptionIdentifierBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();

            Body Body5 = NodeHelper.CreateInitializedBody(typeof(DeferredBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList);
            Assert.That(Body5 is DeferredBody);

            IBlockList<EntityDeclaration> EmptyEntityDeclarationBlockList = BlockListHelper.CreateEmptyBlockList<EntityDeclaration>();
            IBlockList<Instruction> EmptyInstructionBlockList = BlockListHelper.CreateEmptyBlockList<Instruction>();
            IBlockList<ExceptionHandler> EmptyExceptionHandlerBlockList = BlockListHelper.CreateEmptyBlockList<ExceptionHandler>();

            Body Body6 = NodeHelper.CreateInitializedBody(typeof(EffectiveBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, EmptyInstructionBlockList, EmptyExceptionHandlerBlockList);
            Assert.That(Body6 is EffectiveBody);

            Body Body7 = NodeHelper.CreateInitializedBody(typeof(ExternBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList);
            Assert.That(Body7 is ExternBody);

            IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper.CreateEmptyReference<ObjectType>();

            Body Body8 = NodeHelper.CreateInitializedBody(typeof(PrecursorBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, null, null, null, AncestorType);
            Assert.That(Body8 is PrecursorBody);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateInitializedBody(typeof(Identifier), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
        }

        [Test]
        public static void TestSpecificObjectsExpression()
        {
            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            AgentExpression Expression1 = NodeHelper.CreateAgentExpression(EmptyIdentifier);
            AgentExpression Expression2 = NodeHelper.CreateAgentExpression(EmptyIdentifier, DefaultObjectType);
            AssertionTagExpression Expression3 = NodeHelper.CreateAssertionTagExpression(EmptyIdentifier);

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            BinaryConditionalExpression Expression4 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.Or, RightExpression);
            BinaryOperatorExpression Expression5 = NodeHelper.CreateBinaryOperatorExpression(LeftExpression, EmptyIdentifier, RightExpression);

            Identifier EmptyClassIdentifier = NodeHelper.CreateEmptyIdentifier();
            Identifier EmptyConstantIdentifier = NodeHelper.CreateEmptyIdentifier();
            ClassConstantExpression Expression6 = NodeHelper.CreateClassConstantExpression(EmptyClassIdentifier, EmptyConstantIdentifier);

            CloneOfExpression Expression7 = NodeHelper.CreateCloneOfExpression(CloneType.Shallow, LeftExpression);

            QualifiedName EmptyQuery = NodeHelper.CreateEmptyQualifiedName();
            EntityExpression Expression8 = NodeHelper.CreateEntityExpression(EmptyQuery);

            EqualityExpression Expression9 = NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Equal, EqualityType.Physical, RightExpression);

            List<Argument> EmptyArgumentList = new();
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            IndexQueryExpression Expression10 = NodeHelper.CreateIndexQueryExpression(LeftExpression, EmptyArgumentList);
            IndexQueryExpression Expression11 = NodeHelper.CreateIndexQueryExpression(LeftExpression, EmptyArgumentBlockList);

            List<AssignmentArgument> EmptyAssignmentArgumentList = new();
            IBlockList<AssignmentArgument> EmptyAssignmentArgumentBlockList = BlockListHelper.CreateEmptyBlockList<AssignmentArgument>();
            InitializedObjectExpression Expression12 = NodeHelper.CreateInitializedObjectExpression(EmptyClassIdentifier, EmptyAssignmentArgumentList);
            InitializedObjectExpression Expression13 = NodeHelper.CreateInitializedObjectExpression(EmptyClassIdentifier, EmptyAssignmentArgumentBlockList);

            KeywordEntityExpression Expression14 = NodeHelper.CreateKeywordEntityExpression(Keyword.Current);
            KeywordExpression Expression15 = NodeHelper.CreateKeywordExpression(Keyword.Current);
            ManifestCharacterExpression Expression16 = NodeHelper.CreateManifestCharacterExpression("*");
            ManifestNumberExpression Expression17 = NodeHelper.CreateDefaultManifestNumberExpression();
            ManifestNumberExpression Expression18 = NodeHelper.CreateSimpleManifestNumberExpression("0");
            ManifestStringExpression Expression19 = NodeHelper.CreateManifestStringExpression(string.Empty);

            NewExpression Expression20 = NodeHelper.CreateNewExpression(EmptyQuery);
            OldExpression Expression21 = NodeHelper.CreateOldExpression(EmptyQuery);
            PrecursorExpression Expression22 = NodeHelper.CreatePrecursorExpression(EmptyArgumentList);
            PrecursorExpression Expression23 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList);
            PrecursorExpression Expression24 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, DefaultObjectType);
            PrecursorIndexExpression Expression25 = NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentList);
            PrecursorIndexExpression Expression26 = NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentBlockList);
            PrecursorIndexExpression Expression27 = NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentBlockList, DefaultObjectType);
            PreprocessorExpression Expression28 = NodeHelper.CreatePreprocessorExpression(PreprocessorMacro.DateAndTime);
            QueryExpression Expression29 = NodeHelper.CreateQueryExpression(EmptyQuery, EmptyArgumentList);
            QueryExpression Expression30 = NodeHelper.CreateQueryExpression(EmptyQuery, EmptyArgumentBlockList);
            ResultOfExpression Expression31 = NodeHelper.CreateResultOfExpression(LeftExpression);
            UnaryNotExpression Expression32 = NodeHelper.CreateUnaryNotExpression(RightExpression);
            UnaryOperatorExpression Expression33 = NodeHelper.CreateUnaryOperatorExpression(EmptyIdentifier, RightExpression);
        }
    }
}
