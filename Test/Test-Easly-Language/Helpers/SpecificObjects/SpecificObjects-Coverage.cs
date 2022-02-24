namespace TestEaslyLanguage;

using System.Collections.Generic;
using ArgumentException = System.ArgumentException;
using BaseNode;
using Range = BaseNode.Range;
using BaseNodeHelper;
using Easly;
using NotNullReflection;
using NUnit.Framework;

[TestFixture]
public partial class SpecificObjectsCoverage
{
    [Test]
    public static void TestArgument()
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

        CheckMustNotBeEmptyException("parameterList", () => { NodeHelper.CreateAssignmentArgument(EmptyIdentifierList, Source); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentArgument(NullIdentifierList , Source); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentArgument(SimpleIdentifierList, NullSource); });
#endif

        IBlockList<Identifier> EmptyBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
        IBlockList<Identifier> SimpleBlockList = BlockListHelper.CreateSimpleBlockList(EmptyIdentifier);

        AssignmentArgument Argument2 = NodeHelper.CreateAssignmentArgument(SimpleBlockList, Source);
        Assert.AreEqual(Argument2.ParameterBlocks, SimpleBlockList);
        Assert.AreEqual(Argument2.Source, Source);

        CheckMustNotBeEmptyException("parameterBlocks", () => { NodeHelper.CreateAssignmentArgument(EmptyBlockList, Source); });

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
    public static void TestBody()
    {
        DeferredBody Body1 = NodeHelper.CreateEmptyDeferredBody();
        EffectiveBody Body2 = NodeHelper.CreateEmptyEffectiveBody();
        ExternBody Body3 = NodeHelper.CreateEmptyExternBody();
        PrecursorBody Body4 = NodeHelper.CreateEmptyPrecursorBody();

        Document Document = NodeHelper.CreateEmptyDocument();
        IBlockList<Assertion> EmptyRequireBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IBlockList<Assertion> EmptyEnsureBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IBlockList<Identifier> EmptyExceptionIdentifierBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();

        Body Body5 = NodeHelper.CreateInitializedBody(Type.FromTypeof<DeferredBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList);
        Assert.That(Body5 is DeferredBody);

#if !DEBUG
        Type NullNodeType = null!;
        Document NullDocument = null!;
        IBlockList<Assertion> NullRequireBlockList = null!;
        IBlockList<Assertion> NullEnsureBlockList = null!;
        IBlockList<Identifier> NullExceptionIdentifierBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(NullNodeType, Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<DeferredBody>(), NullDocument, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<DeferredBody>(), Document, NullRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<DeferredBody>(), Document, EmptyRequireBlockList, NullEnsureBlockList, EmptyExceptionIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<DeferredBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, NullExceptionIdentifierBlockList); });
#endif

        IBlockList<EntityDeclaration> EmptyEntityDeclarationBlockList = BlockListHelper.CreateEmptyBlockList<EntityDeclaration>();
        IBlockList<Instruction> EmptyInstructionBlockList = BlockListHelper.CreateEmptyBlockList<Instruction>();
        IBlockList<ExceptionHandler> EmptyExceptionHandlerBlockList = BlockListHelper.CreateEmptyBlockList<ExceptionHandler>();

        Body Body6 = NodeHelper.CreateInitializedBody(Type.FromTypeof<EffectiveBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, EmptyInstructionBlockList, EmptyExceptionHandlerBlockList);
        Assert.That(Body6 is EffectiveBody);

#if !DEBUG
        IBlockList<EntityDeclaration> NullEntityDeclarationBlockList = null!;
        IBlockList<Instruction> NullInstructionBlockList = null!;
        IBlockList<ExceptionHandler> NullExceptionHandlerBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<EffectiveBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, NullEntityDeclarationBlockList, EmptyInstructionBlockList, EmptyExceptionHandlerBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<EffectiveBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, NullInstructionBlockList, EmptyExceptionHandlerBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<EffectiveBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, EmptyEntityDeclarationBlockList, EmptyInstructionBlockList, NullExceptionHandlerBlockList); });
#endif

        Body Body7 = NodeHelper.CreateInitializedBody(Type.FromTypeof<ExternBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList);
        Assert.That(Body7 is ExternBody);

        IOptionalReference<ObjectType> AncestorType = OptionalReferenceHelper.CreateReference(NodeHelper.CreateDefaultObjectType());

        Body Body8 = NodeHelper.CreateInitializedBody(Type.FromTypeof<PrecursorBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, null, null, null, AncestorType);
        Assert.That(Body8 is PrecursorBody);

        CheckWrongTypeException("nodeType", Type.FromTypeof<Body>(), () => { NodeHelper.CreateInitializedBody(Type.FromTypeof<Identifier>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList); });

#if !DEBUG
        IOptionalReference<ObjectType> NullAncestorType = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedBody(Type.FromTypeof<PrecursorBody>(), Document, EmptyRequireBlockList, EmptyEnsureBlockList, EmptyExceptionIdentifierBlockList, null, null, null, NullAncestorType); });
#endif
    }

    [Test]
    public static void TestExpression()
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

        CheckMustNotBeEmptyException("argumentList", () => { NodeHelper.CreateIndexQueryExpression(LeftExpression, EmptyArgumentList); });
        CheckMustNotBeEmptyException("argumentBlocks", () => { NodeHelper.CreateIndexQueryExpression(LeftExpression, EmptyArgumentBlockList); });

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

        CheckMustNotBeEmptyException("argumentList", () => { NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentList); });
        CheckMustNotBeEmptyException("argumentBlocks", () => { NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentBlockList); });
        CheckMustNotBeEmptyException("argumentBlocks", () => { NodeHelper.CreatePrecursorIndexExpression(EmptyArgumentBlockList, DefaultObjectType); });

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

    [Test]
    public static void TestFeature()
    {
        AttributeFeature Feature1 = NodeHelper.CreateEmptyAttributeFeature();
        ConstantFeature Feature2 = NodeHelper.CreateEmptyConstantFeature();
        CreationFeature Feature3 = NodeHelper.CreateEmptyCreationFeature();
        FunctionFeature Feature4 = NodeHelper.CreateEmptyFunctionFeature();
        IndexerFeature Feature5 = NodeHelper.CreateEmptyIndexerFeature();
        ProcedureFeature Feature6 = NodeHelper.CreateEmptyProcedureFeature();
        PropertyFeature Feature7 = NodeHelper.CreateEmptyPropertyFeature();

        Document EmptyDocument = NodeHelper.CreateEmptyDocument();
        Identifier EmptyExportIdentifier = NodeHelper.CreateEmptyIdentifier();
        Name EmptyName = NodeHelper.CreateEmptyName();
        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        IBlockList<Assertion> EmptyEnsureBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        IBlockList<CommandOverload> EmptyCommandOverloadBlockList = BlockListHelper.CreateEmptyBlockList<CommandOverload>();
        CommandOverload DefaultCommandOverload = NodeHelper.CreateEmptyCommandOverload();
        IBlockList<CommandOverload> SimpleCommandOverloadBlockList = BlockListHelper.CreateSimpleBlockList(DefaultCommandOverload);
        IBlockList<QueryOverload> EmptyQueryOverloadBlockList = BlockListHelper.CreateEmptyBlockList<QueryOverload>();
        QueryOverload DefaultQueryOverload = NodeHelper.CreateEmptyQueryOverload();
        IBlockList<QueryOverload> SimpleQueryOverloadBlockList = BlockListHelper.CreateSimpleBlockList(DefaultQueryOverload);
        IBlockList<Identifier> EmptyIdentifierBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
        IOptionalReference<Body> EmptyGetter = OptionalReferenceHelper.CreateReference(NodeHelper.CreateDefaultBody());
        IOptionalReference<Body> EmptySetter = OptionalReferenceHelper.CreateReference(NodeHelper.CreateDefaultBody());
        IBlockList<EntityDeclaration> EmptyEntityDeclarationBlockList = BlockListHelper.CreateEmptyBlockList<EntityDeclaration>();
        EntityDeclaration DefaultEntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();
        IBlockList<EntityDeclaration> SimpleEntityDeclarationBlockList = BlockListHelper.CreateSimpleBlockList(DefaultEntityDeclaration);

        Feature Feature8 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<AttributeFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, EmptyEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed);
        Feature Feature9 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<ConstantFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, DefaultExpression, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed);
        Feature Feature10 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<CreationFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed);
        Feature Feature11 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<FunctionFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, null, OnceChoice.Normal, SimpleQueryOverloadBlockList, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed);
        Feature Feature12 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<ProcedureFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed);
        Feature Feature13 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, null, ParameterEndStatus.Closed);
        Feature Feature14 = NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed);

        CheckWrongTypeException("nodeType", Type.FromTypeof<Feature>(), () => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<Identifier>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, null, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        CheckMustNotBeEmptyException("commandOverloadBlocks", () => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<CreationFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, EmptyCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        CheckMustNotBeEmptyException("queryOverloadBlocks", () => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<FunctionFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, null, OnceChoice.Normal, EmptyQueryOverloadBlockList, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        CheckMustNotBeEmptyException("commandOverloadBlocks", () => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ProcedureFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, EmptyCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        CheckMustNotBeEmptyException("indexParameterBlocks", () => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, EmptyEntityDeclarationBlockList, ParameterEndStatus.Closed); });

#if !DEBUG
        Document NullDocument = null!;
        Identifier NullExportIdentifier = null!;
        Name NullName = null!;
        ObjectType NullObjectType = null!;
        IBlockList<Assertion> NullEnsureBlockList = null!;
        Expression NullExpression = null!;
        IBlockList<CommandOverload> NullCommandOverloadBlockList = null!;
        IBlockList<QueryOverload> NullQueryOverloadBlockList = null!;
        IBlockList<Identifier> NullIdentifierBlockList = null!;
        IOptionalReference<Body> NullBodyReference = null!;
        IBlockList<EntityDeclaration> NullEntityDeclarationBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(null!, EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, EmptyEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<AttributeFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, EmptyEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<AttributeFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, EmptyEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<AttributeFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, NullName, DefaultObjectType, EmptyEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<AttributeFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, NullObjectType, NullEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<AttributeFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, NullEnsureBlockList, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ConstantFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, DefaultExpression, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ConstantFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, DefaultExpression, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ConstantFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, NullName, DefaultObjectType, null, DefaultExpression, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ConstantFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, NullObjectType, null, DefaultExpression, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ConstantFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, NullExpression, null, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<CreationFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<CreationFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<CreationFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, NullName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<CreationFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, NullCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<FunctionFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, null, OnceChoice.Normal, SimpleQueryOverloadBlockList, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<FunctionFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, null, OnceChoice.Normal, SimpleQueryOverloadBlockList, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<FunctionFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, NullName, null, null, null, null, OnceChoice.Normal, SimpleQueryOverloadBlockList, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<FunctionFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, null, OnceChoice.Normal, NullQueryOverloadBlockList, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ProcedureFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ProcedureFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ProcedureFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, NullName, null, null, null, SimpleCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<ProcedureFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, null, null, null, NullCommandOverloadBlockList, OnceChoice.Normal, null, UtilityType.ReadOnly, null, null, null, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, NullName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, NullObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, NullIdentifierBlockList, EmptyGetter, EmptySetter, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, NullBodyReference, EmptySetter, null, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<PropertyFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, EmptyName, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, NullBodyReference, null, ParameterEndStatus.Closed); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), NullDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, NullExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, NullObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, NullIdentifierBlockList, EmptyGetter, EmptySetter, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, NullBodyReference, EmptySetter, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, NullBodyReference, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInitializedFeature(Type.FromTypeof<IndexerFeature>(), EmptyDocument, EmptyExportIdentifier, ExportStatus.Exported, null, DefaultObjectType, null, null, null, OnceChoice.Normal, null, UtilityType.ReadOnly, EmptyIdentifierBlockList, EmptyGetter, EmptySetter, NullEntityDeclarationBlockList, ParameterEndStatus.Closed); });
#endif
    }

    [Test]
    public static void TestInstruction()
    {
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        IBlockList<Continuation> EmptyContinuationBlockList = BlockListHelper.CreateEmptyBlockList<Continuation>();
        Continuation EmptyContinuation = NodeHelper.CreateEmptyContinuation();
        IBlockList<Continuation> SimpleContinuationBlockList = BlockListHelper.CreateSimpleBlockList(EmptyContinuation);
        Scope EmptyScope = NodeHelper.CreateEmptyScope();
        Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();

#if !DEBUG
        Expression NullExpression = null!;
        Continuation NullContinuation = null!;
        IBlockList<Continuation> NullContinuationBlockList = null!;
        Scope NullScope = null!;
        Instruction NullInstruction = null!;
#endif

        AsLongAsInstruction Instruction1 = NodeHelper.CreateAsLongAsInstruction(DefaultExpression, EmptyContinuation);
        AsLongAsInstruction Instruction2 = NodeHelper.CreateAsLongAsInstruction(DefaultExpression, SimpleContinuationBlockList);
        AsLongAsInstruction Instruction3 = NodeHelper.CreateAsLongAsInstruction(DefaultExpression, SimpleContinuationBlockList, EmptyScope);

        CheckMustNotBeEmptyException("continuationBlocks", () => { NodeHelper.CreateAsLongAsInstruction(DefaultExpression, EmptyContinuationBlockList); });
        CheckMustNotBeEmptyException("continuationBlocks", () => { NodeHelper.CreateAsLongAsInstruction(DefaultExpression, EmptyContinuationBlockList, EmptyScope); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(NullExpression, EmptyContinuation); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(DefaultExpression, NullContinuation); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(NullExpression, SimpleContinuationBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(DefaultExpression, NullContinuationBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(NullExpression, SimpleContinuationBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(DefaultExpression, NullContinuationBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAsLongAsInstruction(DefaultExpression, SimpleContinuationBlockList, NullScope); });
#endif

        List<QualifiedName> EmptyQualifiedNameList = new();
        QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
        List<QualifiedName> SimpleQualifiedNameList = new() { EmptyQualifiedName };
        IBlockList<QualifiedName> EmptyQualifiedNameBlockList = BlockListHelper.CreateEmptyBlockList<QualifiedName>();
        IBlockList<QualifiedName> SimpleQualifiedNameBlockList = BlockListHelper.CreateSimpleBlockList(EmptyQualifiedName);

        AssignmentInstruction Instruction4 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, DefaultExpression);
        AssignmentInstruction Instruction5 = NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameBlockList, DefaultExpression);

        CheckMustNotBeEmptyException("assignmentList", () => { NodeHelper.CreateAssignmentInstruction(EmptyQualifiedNameList, DefaultExpression); });
        CheckMustNotBeEmptyException("assignmentBlocks", () => { NodeHelper.CreateAssignmentInstruction(EmptyQualifiedNameBlockList, DefaultExpression); });

#if !DEBUG
        List<QualifiedName> NullQualifiedNameList = null!;
        IBlockList<QualifiedName> NullQualifiedNameBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentInstruction(NullQualifiedNameList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameList, NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentInstruction(NullQualifiedNameBlockList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentInstruction(SimpleQualifiedNameBlockList, NullExpression); });
#endif

        List<Name> EmptyNameList = new();
        Name EmptyName = NodeHelper.CreateEmptyName();
        List<Name> SimpleNameList = new() { EmptyName };
        IBlockList<Name> EmptyNameBlockList = BlockListHelper.CreateEmptyBlockList<Name>();
        IBlockList<Name> SimpleNameBlockList = BlockListHelper.CreateSimpleBlockList(EmptyName);
        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        IBlockList<Attachment> EmptyAttachmentBlockList = BlockListHelper.CreateEmptyBlockList<Attachment>();
        Attachment DefaultAttachment = NodeHelper.CreateAttachment(DefaultObjectType);
        IBlockList<Attachment> SimpleAttachmentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultAttachment);
        AttachmentInstruction Instruction6 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameList);
        AttachmentInstruction Instruction7 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, SimpleAttachmentBlockList);
        AttachmentInstruction Instruction8 = NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, SimpleAttachmentBlockList, EmptyScope);

        CheckMustNotBeEmptyException("nameList", () => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, EmptyNameList); });
        CheckMustNotBeEmptyException("entityNameBlocks", () => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, EmptyNameBlockList, SimpleAttachmentBlockList); });
        CheckMustNotBeEmptyException("attachmentBlocks", () => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, EmptyAttachmentBlockList); });
        CheckMustNotBeEmptyException("entityNameBlocks", () => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, EmptyNameBlockList, SimpleAttachmentBlockList, EmptyScope); });
        CheckMustNotBeEmptyException("attachmentBlocks", () => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, EmptyAttachmentBlockList, EmptyScope); });

#if !DEBUG
        List<Name> NullNameList = null!;
        IBlockList<Name> NullNameBlockList = null!;
        IBlockList<Attachment> NullAttachmentBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(NullExpression, SimpleNameList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, NullNameList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(NullExpression, SimpleNameBlockList, SimpleAttachmentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, NullNameBlockList, SimpleAttachmentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, NullAttachmentBlockList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(NullExpression, SimpleNameBlockList, SimpleAttachmentBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, NullNameBlockList, SimpleAttachmentBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, NullAttachmentBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachmentInstruction(DefaultExpression, SimpleNameBlockList, SimpleAttachmentBlockList, NullScope); });
#endif

        CheckInstruction Instruction9 = NodeHelper.CreateCheckInstruction(DefaultExpression);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCheckInstruction(NullExpression); });
#endif

        List<Argument> EmptyArgumentList = new();
        IBlockList<Argument> EmptyArgumentBlockList = BlockListHelper.CreateEmptyBlockList<Argument>();
        CommandInstruction Instruction10 = NodeHelper.CreateCommandInstruction(EmptyQualifiedName, EmptyArgumentList);
        CommandInstruction Instruction11 = NodeHelper.CreateCommandInstruction(EmptyQualifiedName, EmptyArgumentBlockList);

#if !DEBUG
        QualifiedName NullQualifiedName = null!;
        List<Argument> NullArgumentList = null!;
        IBlockList<Argument> NullArgumentBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCommandInstruction(NullQualifiedName, EmptyArgumentList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCommandInstruction(EmptyQualifiedName, NullArgumentList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCommandInstruction(NullQualifiedName, EmptyArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCommandInstruction(EmptyQualifiedName, NullArgumentBlockList); });
#endif

        Identifier EmptyEntityIdentifier = NodeHelper.CreateEmptyIdentifier();
        Identifier CreationRoutineIdentifier = NodeHelper.CreateEmptyIdentifier();

        CreateInstruction Instruction12 = NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, EmptyArgumentList);
        CreateInstruction Instruction13 = NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, EmptyArgumentBlockList);
        CreateInstruction Instruction14 = NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, EmptyArgumentBlockList, EmptyQualifiedName);

#if !DEBUG
        Identifier NullIdentifier = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(NullIdentifier, CreationRoutineIdentifier, EmptyArgumentList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, NullIdentifier, EmptyArgumentList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, NullArgumentList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(NullIdentifier, CreationRoutineIdentifier, EmptyArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, NullIdentifier, EmptyArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, NullArgumentBlockList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(NullIdentifier, CreationRoutineIdentifier, EmptyArgumentBlockList, EmptyQualifiedName); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, NullIdentifier, EmptyArgumentBlockList, EmptyQualifiedName); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, NullArgumentBlockList, EmptyQualifiedName); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateCreateInstruction(EmptyEntityIdentifier, CreationRoutineIdentifier, EmptyArgumentBlockList, NullQualifiedName); });
#endif

        DebugInstruction Instruction15 = NodeHelper.CreateEmptyDebugInstruction();
        DebugInstruction Instruction16 = NodeHelper.CreateSimpleDebugInstruction(DefaultInstruction);
        DebugInstruction Instruction16_1 = NodeHelper.CreateDebugInstruction(EmptyScope);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleDebugInstruction(NullInstruction); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateDebugInstruction(NullScope); });
#endif

        ForLoopInstruction Instruction17 = NodeHelper.CreateEmptyForLoopInstruction();
        ForLoopInstruction Instruction18 = NodeHelper.CreateSimpleForLoopInstruction(DefaultInstruction);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleForLoopInstruction(NullInstruction); });
#endif

        Conditional EmptyConditional = NodeHelper.CreateEmptyConditional();
        IfThenElseInstruction Instruction19 = NodeHelper.CreateIfThenElseInstruction(EmptyConditional);

#if !DEBUG
        Conditional NullConditional = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIfThenElseInstruction(NullConditional); });
#endif

        IBlockList<Conditional> EmptyConditionalBlockList = BlockListHelper.CreateEmptyBlockList<Conditional>();
        IBlockList<Conditional> SimpleConditionalBlockList = BlockListHelper.CreateSimpleBlockList(EmptyConditional);
        IfThenElseInstruction Instruction20 = NodeHelper.CreateIfThenElseInstruction(SimpleConditionalBlockList);
        IfThenElseInstruction Instruction21 = NodeHelper.CreateIfThenElseInstruction(SimpleConditionalBlockList, EmptyScope);

        CheckMustNotBeEmptyException("conditionalBlocks", () => { NodeHelper.CreateIfThenElseInstruction(EmptyConditionalBlockList); });
        CheckMustNotBeEmptyException("conditionalBlocks", () => { NodeHelper.CreateIfThenElseInstruction(EmptyConditionalBlockList, EmptyScope); });

#if !DEBUG
        IBlockList<Conditional> NullConditionalBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIfThenElseInstruction(NullConditionalBlockList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIfThenElseInstruction(NullConditionalBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIfThenElseInstruction(SimpleConditionalBlockList, NullScope); });
#endif

        Argument DefaultArgument = NodeHelper.CreateDefaultArgument();
        List<Argument> SimpleArgumentList = new() { DefaultArgument };
        IBlockList<Argument> SimpleArgumentBlockList = BlockListHelper.CreateSimpleBlockList<Argument>(DefaultArgument);
        IndexAssignmentInstruction Instruction22 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentList, DefaultExpression);
        IndexAssignmentInstruction Instruction23 = NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentBlockList, DefaultExpression);

        CheckMustNotBeEmptyException("argumentList", () => { NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, EmptyArgumentList, DefaultExpression); });
        CheckMustNotBeEmptyException("argumentBlocks", () => { NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, EmptyArgumentBlockList, DefaultExpression); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexAssignmentInstruction(NullQualifiedName, SimpleArgumentList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, NullArgumentList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentList, NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexAssignmentInstruction(NullQualifiedName, SimpleArgumentBlockList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, NullArgumentBlockList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexAssignmentInstruction(EmptyQualifiedName, SimpleArgumentBlockList, NullExpression); });
#endif

        InspectInstruction Instruction24 = NodeHelper.CreateInspectInstruction(DefaultExpression);

        Expression ConstantExpression = NodeHelper.CreateDefaultExpression();
        With SimpleWith = NodeHelper.CreateSimpleWith(ConstantExpression);
        InspectInstruction Instruction25 = NodeHelper.CreateInspectInstruction(DefaultExpression, SimpleWith);

        IBlockList<With> EmptyWithBlockList = BlockListHelper.CreateEmptyBlockList<With>();
        IBlockList<With> SimpleWithBlockList = BlockListHelper.CreateSimpleBlockList(SimpleWith);

        InspectInstruction Instruction26 = NodeHelper.CreateInspectInstruction(ConstantExpression, SimpleWithBlockList);
        InspectInstruction Instruction27 = NodeHelper.CreateInspectInstruction(ConstantExpression, SimpleWithBlockList, EmptyScope);

        CheckMustNotBeEmptyException("withBlocks", () => { NodeHelper.CreateInspectInstruction(ConstantExpression, EmptyWithBlockList); });
        CheckMustNotBeEmptyException("withBlocks", () => { NodeHelper.CreateInspectInstruction(ConstantExpression, EmptyWithBlockList, EmptyScope); });

#if !DEBUG
        With NullWith = null!;
        IBlockList<With> NullWithBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(NullExpression, SimpleWith); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(DefaultExpression, NullWith); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(NullExpression, SimpleWithBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(ConstantExpression, NullWithBlockList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(NullExpression, SimpleWithBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(ConstantExpression, NullWithBlockList, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateInspectInstruction(ConstantExpression, SimpleWithBlockList, NullScope); });
#endif

        KeywordAssignmentInstruction Instruction28 = NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, DefaultExpression);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateKeywordAssignmentInstruction(Keyword.Result, NullExpression); });
#endif

        OverLoopInstruction Instruction29 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameList);
        OverLoopInstruction Instruction30 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameList, DefaultInstruction);

        IBlockList<Assertion> EmptyAssertionBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        OverLoopInstruction Instruction31 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, EmptyAssertionBlockList);
        OverLoopInstruction Instruction32 = NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, EmptyIdentifier, EmptyAssertionBlockList);

        CheckMustNotBeEmptyException("nameList", () => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, EmptyNameList); });
        CheckMustNotBeEmptyException("nameList", () => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, EmptyNameList, DefaultInstruction); });
        CheckMustNotBeEmptyException("indexerBlocks", () => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, EmptyNameBlockList, IterationType.Single, EmptyScope, EmptyAssertionBlockList); });
        CheckMustNotBeEmptyException("indexerBlocks", () => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, EmptyNameBlockList, IterationType.Single, EmptyScope, EmptyIdentifier, EmptyAssertionBlockList); });

#if !DEBUG
        IBlockList<Assertion> NullAssertionBlockList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(NullExpression, SimpleNameList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, NullNameList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(NullExpression, SimpleNameList, DefaultInstruction); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, NullNameList, DefaultInstruction); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameList, NullInstruction); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(NullExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, NullNameBlockList, IterationType.Single, EmptyScope, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, NullScope, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, NullAssertionBlockList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(NullExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, EmptyIdentifier, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, NullNameBlockList, IterationType.Single, EmptyScope, EmptyIdentifier, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, NullScope, EmptyIdentifier, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, NullIdentifier, EmptyAssertionBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateOverLoopInstruction(DefaultExpression, SimpleNameBlockList, IterationType.Single, EmptyScope, EmptyIdentifier, NullAssertionBlockList); });
#endif

        PrecursorIndexAssignmentInstruction Instruction33 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(SimpleArgumentList, DefaultExpression);
        PrecursorIndexAssignmentInstruction Instruction34 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(SimpleArgumentBlockList, DefaultExpression);
        PrecursorIndexAssignmentInstruction Instruction35 = NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, SimpleArgumentBlockList, DefaultExpression);

        CheckMustNotBeEmptyException("argumentList", () => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(EmptyArgumentList, DefaultExpression); });
        CheckMustNotBeEmptyException("argumentBlocks", () => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(EmptyArgumentBlockList, DefaultExpression); });
        CheckMustNotBeEmptyException("argumentBlocks", () => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, EmptyArgumentBlockList, DefaultExpression); });

#if !DEBUG
        ObjectType NullObjectType = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(NullArgumentList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(EmptyArgumentList, NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(NullArgumentBlockList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(EmptyArgumentBlockList, NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(NullObjectType, EmptyArgumentBlockList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, NullArgumentBlockList, DefaultExpression); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorIndexAssignmentInstruction(DefaultObjectType, EmptyArgumentBlockList, NullExpression); });
#endif

        PrecursorInstruction Instruction36 = NodeHelper.CreatePrecursorInstruction(EmptyArgumentList);
        PrecursorInstruction Instruction37 = NodeHelper.CreatePrecursorInstruction(EmptyArgumentBlockList);
        PrecursorInstruction Instruction38 = NodeHelper.CreatePrecursorInstruction(DefaultObjectType, EmptyArgumentBlockList);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorInstruction(NullArgumentList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorInstruction(NullArgumentBlockList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorInstruction(NullObjectType, EmptyArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePrecursorInstruction(DefaultObjectType, NullArgumentBlockList); });
#endif

        RaiseEventInstruction Instruction39 = NodeHelper.CreateRaiseEventInstruction(EmptyIdentifier);
        ReleaseInstruction Instruction40 = NodeHelper.CreateReleaseInstruction(EmptyQualifiedName);
        ThrowInstruction Instruction41 = NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, EmptyArgumentList);
        ThrowInstruction Instruction42 = NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, EmptyArgumentBlockList);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRaiseEventInstruction(NullIdentifier); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateReleaseInstruction(NullQualifiedName); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateThrowInstruction(NullObjectType, EmptyIdentifier, EmptyArgumentList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateThrowInstruction(DefaultObjectType, NullIdentifier, EmptyArgumentList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, NullArgumentList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateThrowInstruction(NullObjectType, EmptyIdentifier, EmptyArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateThrowInstruction(DefaultObjectType, NullIdentifier, EmptyArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateThrowInstruction(DefaultObjectType, EmptyIdentifier, NullArgumentBlockList); });
#endif
    }

    [Test]
    public static void TestOther()
    {
        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();
        Scope EmptyScope = NodeHelper.CreateEmptyScope();
        Expression DefaultExpression = NodeHelper.CreateDefaultExpression();
        Instruction DefaultInstruction = NodeHelper.CreateDefaultInstruction();
        Name EmptyName = NodeHelper.CreateEmptyName();

#if !DEBUG
        ObjectType NullObjectType = null!;
        Scope NullScope = null!;
        Expression NullExpression = null!;
        Instruction NullInstruction = null!;
        Name NullName = null!;
        string NullText = null!;
#endif

        IBlockList<ObjectType> EmptyObjectTypeBlockList = BlockListHelper.CreateEmptyBlockList<ObjectType>();
        IBlockList<ObjectType> SimpleObjectTypeBlockList = BlockListHelper.CreateSimpleBlockList(DefaultObjectType);

        Attachment Attachment1 = NodeHelper.CreateAttachment(DefaultObjectType);
        Attachment Attachment2 = NodeHelper.CreateAttachment(SimpleObjectTypeBlockList, EmptyScope);

        CheckMustNotBeEmptyException("attachTypeBlocks", () => { NodeHelper.CreateAttachment(EmptyObjectTypeBlockList, EmptyScope); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAttachment(NullObjectType); });
#endif

        Conditional Conditional1 = NodeHelper.CreateConditional(DefaultExpression);
        Conditional Conditional2 = NodeHelper.CreateConditional(DefaultExpression, DefaultInstruction);
        Conditional Conditional3 = NodeHelper.CreateConditional(DefaultExpression, EmptyScope);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateConditional(NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateConditional(NullExpression, DefaultInstruction); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateConditional(DefaultExpression, NullInstruction); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateConditional(NullExpression, EmptyScope); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateConditional(DefaultExpression, NullScope); });
#endif

        With With1 = NodeHelper.CreateSimpleWith(DefaultExpression);
        With With2 = NodeHelper.CreateSimpleWith(DefaultExpression, DefaultInstruction);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleWith(NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleWith(NullExpression, DefaultInstruction); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleWith(DefaultExpression, NullInstruction); });
#endif

        Range Range1 = NodeHelper.CreateSingleRange(DefaultExpression);
        EntityDeclaration EntityDeclaration1 = NodeHelper.CreateEntityDeclaration(EmptyName, DefaultObjectType);
        Identifier Identifier1 = NodeHelper.CreateEmptyExportIdentifier();
        Export Export1 = NodeHelper.CreateSimpleExport(string.Empty);
        Class Class1 = NodeHelper.CreateSimpleClass(string.Empty);
        Library Library1 = NodeHelper.CreateSimpleLibrary(string.Empty);
        GlobalReplicate GlobalReplicate1 = NodeHelper.CreateSimpleGlobalReplicate(string.Empty);
        ClassReplicate ClassReplicate1 = NodeHelper.CreateSimpleClassReplicate(string.Empty);
        Import Import1 = NodeHelper.CreateSimpleImport(string.Empty, string.Empty, ImportType.Latest);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSingleRange(NullExpression); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateEntityDeclaration(NullName, DefaultObjectType); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateEntityDeclaration(EmptyName, NullObjectType); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleExport(NullText); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleClass(NullText); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleGlobalReplicate(NullText); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleClassReplicate(NullText); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleImport(NullText, string.Empty, ImportType.Latest); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleImport(string.Empty, NullText, ImportType.Latest); });
#endif

        List<Class> EmptyClassList = new();
        List<Library> EmptyLibraryList = new();
        List<GlobalReplicate> EmptyGlobalReplicateList = new();
        Root Root1 = NodeHelper.CreateRoot(EmptyClassList, EmptyLibraryList, EmptyGlobalReplicateList);

        List<IBlock<Class>> EmptyBlockClassList = new();
        List<IBlock<Library>> EmptyBlockLibraryList = new();
        Root Root2 = NodeHelper.CreateRoot(EmptyBlockClassList, EmptyBlockLibraryList, EmptyGlobalReplicateList);

#if !DEBUG
        List<Class> NullClassList = null!;
        List<Library> NullLibraryList = null!;
        List<GlobalReplicate> NullGlobalReplicateList = null!;
        List<IBlock<Class>> NullBlockClassList = null!;
        List<IBlock<Library>> NullBlockLibraryList = null!;

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRoot(NullClassList, EmptyLibraryList, EmptyGlobalReplicateList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRoot(EmptyClassList, NullLibraryList, EmptyGlobalReplicateList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRoot(EmptyClassList, EmptyLibraryList, NullGlobalReplicateList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRoot(NullBlockClassList, EmptyBlockLibraryList, EmptyGlobalReplicateList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRoot(EmptyBlockClassList, NullBlockLibraryList, EmptyGlobalReplicateList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateRoot(EmptyBlockClassList, EmptyBlockLibraryList, NullGlobalReplicateList); });
#endif
    }

    [Test]
    public static void TestObjectType()
    {
        QualifiedName EmptyQualifiedName = NodeHelper.CreateEmptyQualifiedName();
        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        EntityDeclaration EmptyEntityDeclaration = NodeHelper.CreateEmptyEntityDeclaration();
        ObjectType DefaultObjectType1 = NodeHelper.CreateDefaultObjectType();
        ObjectType DefaultObjectType2 = NodeHelper.CreateDefaultObjectType();
        IBlockList<QueryOverloadType> EmptyQueryOverloadTypeBlockList = BlockListHelper.CreateEmptyBlockList<QueryOverloadType>();
        QueryOverloadType DefaultQueryOverloadType = NodeHelper.CreateEmptyQueryOverloadType(DefaultObjectType2);
        IBlockList<QueryOverloadType> SimpleQueryOverloadTypeBlockList = BlockListHelper.CreateSimpleBlockList(DefaultQueryOverloadType);
        List<TypeArgument> EmptyTypeArgumentList = new();
        TypeArgument DefaultTypeArgument = NodeHelper.CreateDefaultTypeArgument();
        IBlockList<TypeArgument> EmptyTypeArgumentBlockList = BlockListHelper.CreateEmptyBlockList<TypeArgument>();
        List<TypeArgument> SimpleTypeArgumentList = new() { DefaultTypeArgument };
        IBlockList<EntityDeclaration> EmptyEntityDeclarationBlockList = BlockListHelper.CreateEmptyBlockList<EntityDeclaration>();
        IBlockList<TypeArgument> SimpleTypeArgumentBlockList = BlockListHelper.CreateSimpleBlockList(DefaultTypeArgument);
        IBlockList<EntityDeclaration> SimpleEntityDeclarationBlockList = BlockListHelper.CreateSimpleBlockList(EmptyEntityDeclaration);
        IBlockList<Assertion> EmptyGetRequireBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IBlockList<Assertion> EmptyGetEnsureBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IBlockList<Identifier> EmptyGetIdentifierBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
        IBlockList<Assertion> EmptySetRequireBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IBlockList<Assertion> EmptySetEnsureBlockList = BlockListHelper.CreateEmptyBlockList<Assertion>();
        IBlockList<Identifier> EmptySetIdentifierBlockList = BlockListHelper.CreateEmptyBlockList<Identifier>();
        IBlockList<CommandOverloadType> EmptyCommandOverloadTypeBlockList = BlockListHelper.CreateEmptyBlockList<CommandOverloadType>();
        CommandOverloadType EmptyCommandOverloadType = NodeHelper.CreateEmptyCommandOverloadType();
        IBlockList<CommandOverloadType> SimpleCommandOverloadTypeBlockList = BlockListHelper.CreateSimpleBlockList(EmptyCommandOverloadType);

#if !DEBUG
        QualifiedName NullQualifiedName = null!;
        Identifier NullIdentifier = null!;
        EntityDeclaration NullEntityDeclaration = null!;
        ObjectType NullObjectType = null!;
        IBlockList<QueryOverloadType> NullQueryOverloadTypeBlockList = null!;
        List<TypeArgument> NullTypeArgumentList = null!;
        IBlockList<TypeArgument> NullTypeArgumentBlockList = null!;
        IBlockList<EntityDeclaration> NullEntityDeclarationBlockList = null!;
        IBlockList<Assertion> NullGetRequireBlockList = null!;
        IBlockList<Assertion> NullGetEnsureBlockList = null!;
        IBlockList<Identifier> NullGetIdentifierBlockList = null!;
        IBlockList<Assertion> NullSetRequireBlockList = null!;
        IBlockList<Assertion> NullSetEnsureBlockList = null!;
        IBlockList<Identifier> NullSetIdentifierBlockList = null!;
        IBlockList<CommandOverloadType> NullCommandOverloadTypeBlockList = null!;
#endif

        AnchoredType AnchoredType1 = NodeHelper.CreateAnchoredType(EmptyQualifiedName, AnchorKinds.Declaration);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAnchoredType(NullQualifiedName, AnchorKinds.Declaration); });
#endif

        FunctionType FunctionType1 = NodeHelper.CreateFunctionType(DefaultObjectType1, DefaultObjectType2);
        FunctionType FunctionType2 = NodeHelper.CreateFunctionType(DefaultObjectType1, SimpleQueryOverloadTypeBlockList);

        CheckMustNotBeEmptyException("overloadBlocks", () => { NodeHelper.CreateFunctionType(DefaultObjectType1, EmptyQueryOverloadTypeBlockList); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateFunctionType(NullObjectType, DefaultObjectType2); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateFunctionType(DefaultObjectType1, NullObjectType); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateFunctionType(NullObjectType, SimpleQueryOverloadTypeBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateFunctionType(DefaultObjectType1, NullQueryOverloadTypeBlockList); });
#endif

        GenericType GenericType1 = NodeHelper.CreateGenericType(EmptyIdentifier, SimpleTypeArgumentList);
        GenericType GenericType2 = NodeHelper.CreateGenericType(SharingType.NotShared, EmptyIdentifier, SimpleTypeArgumentBlockList);

        CheckMustNotBeEmptyException("typeArgumentList", () => { NodeHelper.CreateGenericType(EmptyIdentifier, EmptyTypeArgumentList); });
        CheckMustNotBeEmptyException("typeArgumentBlocks", () => { NodeHelper.CreateGenericType(SharingType.NotShared, EmptyIdentifier, EmptyTypeArgumentBlockList); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateGenericType(NullIdentifier, SimpleTypeArgumentList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateGenericType(EmptyIdentifier, NullTypeArgumentList); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateGenericType(SharingType.NotShared, NullIdentifier, SimpleTypeArgumentBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateGenericType(SharingType.NotShared, EmptyIdentifier, NullTypeArgumentBlockList); });
#endif

        IndexerType IndexerType1 = NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, EmptyEntityDeclaration);

        IndexerType IndexerType2 = NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList);

        CheckMustNotBeEmptyException("indexParameterBlocks", () => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, EmptyEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(NullObjectType, DefaultObjectType2, EmptyEntityDeclaration); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, NullObjectType, EmptyEntityDeclaration); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, NullEntityDeclaration); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(NullObjectType, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, NullObjectType, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, NullEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, NullGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, NullGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, NullGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, NullSetRequireBlockList, EmptySetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, NullSetEnsureBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateIndexerType(DefaultObjectType1, DefaultObjectType2, SimpleEntityDeclarationBlockList, ParameterEndStatus.Closed, UtilityType.ReadWrite, EmptyGetRequireBlockList, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetEnsureBlockList, NullSetIdentifierBlockList); });
#endif

        KeywordAnchoredType KeywordAnchoredType1 = NodeHelper.CreateKeywordAnchoredType(Keyword.Current);

        ProcedureType ProcedureType1 = NodeHelper.CreateProcedureType(DefaultObjectType1);
        ProcedureType ProcedureType2 = NodeHelper.CreateProcedureType(DefaultObjectType1, SimpleCommandOverloadTypeBlockList);

        CheckMustNotBeEmptyException("overloadBlocks", () => { NodeHelper.CreateProcedureType(DefaultObjectType1, EmptyCommandOverloadTypeBlockList); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateProcedureType(NullObjectType); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateProcedureType(NullObjectType, SimpleCommandOverloadTypeBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateProcedureType(DefaultObjectType1, NullCommandOverloadTypeBlockList); });
#endif

        PropertyType PropertyType1 = NodeHelper.CreatePropertyType(DefaultObjectType1, DefaultObjectType2);
        PropertyType PropertyType2 = NodeHelper.CreatePropertyType(DefaultObjectType1, DefaultObjectType2, UtilityType.ReadWrite, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetIdentifierBlockList);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(NullObjectType, DefaultObjectType2); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(DefaultObjectType1, NullObjectType); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(NullObjectType, DefaultObjectType2, UtilityType.ReadWrite, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(DefaultObjectType1, NullObjectType, UtilityType.ReadWrite, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(DefaultObjectType1, DefaultObjectType2, UtilityType.ReadWrite, NullGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(DefaultObjectType1, DefaultObjectType2, UtilityType.ReadWrite, EmptyGetEnsureBlockList, NullGetIdentifierBlockList, EmptySetRequireBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(DefaultObjectType1, DefaultObjectType2, UtilityType.ReadWrite, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, NullSetRequireBlockList, EmptySetIdentifierBlockList); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePropertyType(DefaultObjectType1, DefaultObjectType2, UtilityType.ReadWrite, EmptyGetEnsureBlockList, EmptyGetIdentifierBlockList, EmptySetRequireBlockList, NullSetIdentifierBlockList); });
#endif

        SimpleType SimpleType1 = NodeHelper.CreateSimpleType(SharingType.NotShared, EmptyIdentifier);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateSimpleType(SharingType.NotShared, NullIdentifier); });
#endif

        TupleType TupleType1 = NodeHelper.CreateTupleType(EmptyEntityDeclaration);
        TupleType TupleType = NodeHelper.CreateTupleType(SharingType.NotShared, SimpleEntityDeclarationBlockList);

        CheckMustNotBeEmptyException("entityDeclarationBlocks", () => { NodeHelper.CreateTupleType(SharingType.NotShared, EmptyEntityDeclarationBlockList); });

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateTupleType(NullEntityDeclaration); });

        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateTupleType(SharingType.NotShared, NullEntityDeclarationBlockList); });
#endif
    }

    [Test]
    public static void TestTypeArgument()
    {
        Identifier EmptyIdentifier = NodeHelper.CreateEmptyIdentifier();
        ObjectType DefaultObjectType = NodeHelper.CreateDefaultObjectType();

#if !DEBUG
        Identifier NullIdentifier = null!;
        ObjectType NullObjectType = null!;
#endif

        AssignmentTypeArgument AssignmentTypeArgument1 = NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, DefaultObjectType);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentTypeArgument(NullIdentifier, DefaultObjectType); });
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreateAssignmentTypeArgument(EmptyIdentifier, NullObjectType); });
#endif

        PositionalTypeArgument PositionalTypeArgument1 = NodeHelper.CreatePositionalTypeArgument(DefaultObjectType);

#if !DEBUG
        Assert.Throws<ArgumentNullException>(() => { NodeHelper.CreatePositionalTypeArgument(NullObjectType); });
#endif
    }

    private static void CheckMustNotBeEmptyException(string parameterName, TestDelegate code)
    {
        ArgumentException? MustNotBeEmptyException = Assert.Throws<ArgumentException>(code);
        Assert.NotNull(MustNotBeEmptyException);
        Assert.AreEqual(MustNotBeEmptyException?.Message, $"{parameterName} must not be empty");
    }

    private static void CheckWrongTypeException(string parameterName, Type baseType, TestDelegate code)
    {
        ArgumentException? WrongTypeException = Assert.Throws<ArgumentException>(code);
        Assert.NotNull(WrongTypeException);
        Assert.AreEqual(WrongTypeException?.Message, $"{parameterName} must inherit from {baseType.FullName}");
    }
}
