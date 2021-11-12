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
#if !DEBUG
            List<Identifier> NullIdentifierList = null!;
            IBlockList<Identifier> NullBlockList = null!;
            Expression NullSource = null!;
#endif

            List<Identifier> EmptyIdentifierList = new();

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            List<Identifier> SimpleIdentifierList = new() { EmptyIdentifier };

            Expression Source = NodeHelper.CreateDefaultExpression();

            AssignmentArgument Argument1 = NodeHelper.CreateAssignmentArgument(SimpleIdentifierList, Source);
            Assert.AreEqual(Argument1.Source, Source);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateAssignmentArgument(EmptyIdentifierList, Source); });

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentArgument(NullIdentifierList , Source); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentArgument(SimpleIdentifierList, NullSource); });
#endif

            IBlockList<Identifier> EmptyBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
            IBlockList<Identifier> SimpleBlockList = BlockListHelper.CreateSimpleBlockList(EmptyIdentifier);

            AssignmentArgument Argument2 = NodeHelper.CreateAssignmentArgument(SimpleBlockList, Source);
            Assert.AreEqual(Argument2.ParameterBlocks, SimpleBlockList);
            Assert.AreEqual(Argument2.Source, Source);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateAssignmentArgument(EmptyBlockList, Source); });

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentArgument(NullBlockList, Source); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentArgument(SimpleBlockList, NullSource); });
#endif

            PositionalArgument Argument3 = NodeHelper.CreatePositionalArgument(Source);
            Assert.AreEqual(Argument3.Source, Source);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePositionalArgument(NullSource); });
#endif
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

#if !DEBUG
            Type NullNodeType = null!;
            Document NullDocumentation = null!;
            IBlockList<Assertion> NullRequireBlockList = null!;
            IBlockList<Assertion> NullEnsureBlockList = null!;
            IBlockList<Identifier> NullExceptionIdentifierBlockList = null!;

            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(NullNodeType, Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(DeferredBody), NullDocumentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(DeferredBody), Documentation, NullRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(DeferredBody), Documentation, EmptyRequireBlockList, NullEnsureBlockList, EmptyExceptionIdentifierBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(DeferredBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, NullExceptionIdentifierBlockList); });
#endif

            IBlockList<EntityDeclaration> EmptyEntityDeclarationBlockList = BlockListHelper.CreateEmptyBlockList<EntityDeclaration>();
            IBlockList<Instruction> EmptyInstructionBlockList = BlockListHelper.CreateEmptyBlockList<Instruction>();
            IBlockList<ExceptionHandler> EmptyExceptionHandlerBlockList = BlockListHelper.CreateEmptyBlockList<ExceptionHandler>();

            Body Body6 = NodeHelper.CreateInitializedBody(typeof(EffectiveBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, EmptyInstructionBlockList, EmptyExceptionHandlerBlockList);
            Assert.That(Body6 is EffectiveBody);

#if !DEBUG
            IBlockList<EntityDeclaration> NullEntityDeclarationBlockList = null!;
            IBlockList<Instruction> NullInstructionBlockList = null!;
            IBlockList<ExceptionHandler> NullExceptionHandlerBlockList = null!;

            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(EffectiveBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, NullEntityDeclarationBlockList, EmptyInstructionBlockList, EmptyExceptionHandlerBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(EffectiveBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, NullInstructionBlockList, EmptyExceptionHandlerBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(EffectiveBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, EmptyInstructionBlockList, NullExceptionHandlerBlockList); });
#endif

            Body Body7 = NodeHelper.CreateInitializedBody(typeof(ExternBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList);
            Assert.That(Body7 is ExternBody);

            IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper.CreateEmptyReference<ObjectType>();

            Body Body8 = NodeHelper.CreateInitializedBody(typeof(PrecursorBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, null, null, null, AncestorType);
            Assert.That(Body8 is PrecursorBody);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateInitializedBody(typeof(Identifier), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });

#if !DEBUG
            IOptionalReference<ObjectType> NullAncestorType = null!;

            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(typeof(PrecursorBody), Documentation, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, null, null, null, NullAncestorType); });
#endif
        }

        [Test]
        public static void TestSpecificObjectsExpression()
        {
#if !DEBUG
            Identifier NullIdentifier = null!;
            ObjectType NullObjectType = null!;
            Expression NullExpression = null!;
            QualifiedName NullQualifiedName = null!;
            List<Argument> NullArgumentList = null!;
            IBlockList<Argument> NullArgumentBlockList = null!;
            List<AssignmentArgument> NullAssignmentArgumentList = null!;
#endif

            Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
            ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

            AgentExpression Expression1 = NodeHelper.CreateAgentExpression(EmptyIdentifier);
            AgentExpression Expression2 = NodeHelper.CreateAgentExpression(EmptyIdentifier, DefaultObjectType);
            AssertionTagExpression Expression3 = NodeHelper.CreateAssertionTagExpression(EmptyIdentifier);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAgentExpression(NullIdentifier); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAgentExpression(EmptyIdentifier, NullObjectType); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssertionTagExpression(NullIdentifier); });
#endif

            Expression LeftExpression = NodeHelper.CreateDefaultExpression();
            Expression RightExpression = NodeHelper.CreateDefaultExpression();

            BinaryConditionalExpression Expression4 = NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.Or, RightExpression);
            BinaryOperatorExpression Expression5 = NodeHelper.CreateBinaryOperatorExpression(LeftExpression, EmptyIdentifier, RightExpression);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateBinaryConditionalExpression(NullExpression, ConditionalTypes.Or, RightExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateBinaryConditionalExpression(LeftExpression, ConditionalTypes.Or, NullExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateBinaryOperatorExpression(NullExpression, EmptyIdentifier, RightExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateBinaryOperatorExpression(LeftExpression, NullIdentifier, RightExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateBinaryOperatorExpression(LeftExpression, EmptyIdentifier, NullExpression); });
#endif

            Identifier EmptyClassIdentifier = NodeHelper.CreateEmptyIdentifier();
            Identifier EmptyConstantIdentifier = NodeHelper.CreateEmptyIdentifier();
            ClassConstantExpression Expression6 = NodeHelper.CreateClassConstantExpression(EmptyClassIdentifier, EmptyConstantIdentifier);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateClassConstantExpression(NullIdentifier, EmptyConstantIdentifier); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateClassConstantExpression(EmptyClassIdentifier, NullIdentifier); });
#endif

            CloneOfExpression Expression7 = NodeHelper.CreateCloneOfExpression(CloneType.Shallow, LeftExpression);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCloneOfExpression(CloneType.Shallow, NullExpression); });
#endif

            QualifiedName EmptyQuery = NodeHelper.CreateEmptyQualifiedName();
            EntityExpression Expression8 = NodeHelper.CreateEntityExpression(EmptyQuery);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateEntityExpression(NullQualifiedName); });
#endif

            EqualityExpression Expression9 = NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Equal, EqualityType.Physical, RightExpression);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateEqualityExpression(NullExpression, ComparisonType.Equal, EqualityType.Physical, RightExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateEqualityExpression(LeftExpression, ComparisonType.Equal, EqualityType.Physical, NullExpression); });
#endif

            List<Argument> EmptyArgumentList = new();
            Argument DefautArgument = NodeHelper.CreateDefaultArgument();
            List<Argument> SimpleArgumentList = new() { DefautArgument };
            IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
            IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefautArgument);
            IndexQueryExpression Expression10 = NodeHelper.CreateIndexQueryExpression(LeftExpression, SimpleArgumentList);
            IndexQueryExpression Expression11 = NodeHelper.CreateIndexQueryExpression(LeftExpression, SimpleArgumentBlockList);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateIndexQueryExpression(LeftExpression, EmptyArgumentList); });
            Assert.Throws<ArgumentException>(() => { NodeHelper.CreateIndexQueryExpression(LeftExpression, EmptyArgumentBlockList); });

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexQueryExpression(NullExpression, SimpleArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexQueryExpression(LeftExpression, NullArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexQueryExpression(LeftExpression, NullArgumentBlockList); });
#endif

            List<AssignmentArgument> EmptyAssignmentArgumentList = new();
            IBlockList<AssignmentArgument> EmptyAssignmentArgumentBlockList = BlockListHelper.CreateEmptyBlockList<AssignmentArgument>();
            InitializedObjectExpression Expression12 = NodeHelper.CreateInitializedObjectExpression(EmptyClassIdentifier, EmptyAssignmentArgumentList);
            InitializedObjectExpression Expression13 = NodeHelper.CreateInitializedObjectExpression(EmptyClassIdentifier, EmptyAssignmentArgumentBlockList);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedObjectExpression(NullIdentifier, EmptyAssignmentArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedObjectExpression(EmptyClassIdentifier, NullAssignmentArgumentList); });
#endif

            KeywordEntityExpression Expression14 = NodeHelper.CreateKeywordEntityExpression(Keyword.Current);
            KeywordExpression Expression15 = NodeHelper.CreateKeywordExpression(Keyword.Current);
            ManifestCharacterExpression Expression16 = NodeHelper.CreateManifestCharacterExpression("*");
            ManifestNumberExpression Expression17 = NodeHelper.CreateDefaultManifestNumberExpression();
            ManifestNumberExpression Expression18 = NodeHelper.CreateSimpleManifestNumberExpression("0");
            ManifestStringExpression Expression19 = NodeHelper.CreateManifestStringExpression(string.Empty);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateManifestCharacterExpression(null!); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleManifestNumberExpression(null!); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateManifestStringExpression(null!); });
#endif

            NewExpression Expression20 = NodeHelper.CreateNewExpression(EmptyQuery);
            OldExpression Expression21 = NodeHelper.CreateOldExpression(EmptyQuery);
            PrecursorExpression Expression22 = NodeHelper.CreatePrecursorExpression(EmptyArgumentList);
            PrecursorExpression Expression23 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList);
            PrecursorExpression Expression24 = NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, DefaultObjectType);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateNewExpression(NullQualifiedName); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOldExpression(NullQualifiedName); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorExpression(NullArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorExpression(NullArgumentBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorExpression(EmptyArgumentBlockList, NullObjectType); });
#endif

            PrecursorIndexExpression Expression25 = NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentList);
            PrecursorIndexExpression Expression26 = NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentBlockList);
            PrecursorIndexExpression Expression27 = NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentBlockList, DefaultObjectType);

            Assert.Throws<ArgumentException>(() => { NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentList); });
            Assert.Throws<ArgumentException>(() => { NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentBlockList); });
            Assert.Throws<ArgumentException>(() => { NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentBlockList, DefaultObjectType); });

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexExpression(NullArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexExpression(NullArgumentBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexExpression(SimpleArgumentBlockList, NullObjectType); });
#endif

            PreprocessorExpression Expression28 = NodeHelper.CreatePreprocessorExpression(PreprocessorMacro.DateAndTime);
            QueryExpression Expression29 = NodeHelper.CreateQueryExpression(EmptyQuery, EmptyArgumentList);
            QueryExpression Expression30 = NodeHelper.CreateQueryExpression(EmptyQuery, EmptyArgumentBlockList);
            ResultOfExpression Expression31 = NodeHelper.CreateResultOfExpression(LeftExpression);
            UnaryNotExpression Expression32 = NodeHelper.CreateUnaryNotExpression(RightExpression);
            UnaryOperatorExpression Expression33 = NodeHelper.CreateUnaryOperatorExpression(EmptyIdentifier, RightExpression);

#if !DEBUG
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateQueryExpression(NullQualifiedName, EmptyArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateQueryExpression(EmptyQuery, NullArgumentList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateQueryExpression(EmptyQuery, NullArgumentBlockList); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateResultOfExpression(NullExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateUnaryNotExpression(NullExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateUnaryOperatorExpression(NullIdentifier, RightExpression); });
            Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateUnaryOperatorExpression(EmptyIdentifier, NullExpression); });
#endif
        }
    }
}
