namespace Test
{
    using BaseNode;
    using Easly;
    using NUnit.Framework;
    using System.Reflection;

    [TestFixture]
    public class TestSet
    {
        [Test]
        public static void TestNodeInitializers()
        {
            Node[] InitializedObjects = new Node[]
            {
                new Assertion(),
                new Attachment(),
                new Class(),
                new ClassReplicate(),
                new CommandOverload(),
                new CommandOverloadType(),
                new Conditional(),
                new Constraint(),
                new Continuation(),
                new Discrete(),
                new EntityDeclaration(),
                new ExceptionHandler(),
                new Export(),
                new ExportChange(),
                new Generic(),
                new GlobalReplicate(),
                new Identifier(),
                new Import(),
                new Inheritance(),
                new Library(),
                new Name(),
                new Pattern(),
                new QualifiedName(),
                new QueryOverload(),
                new QueryOverloadType(),
                new Range(),
                new Rename(),
                new Root(),
                new Scope(),
                new Typedef(),
                new With(),
                new AssignmentArgument(),
                new PositionalArgument(),
                new DeferredBody(),
                new EffectiveBody(),
                new ExternBody(),
                new PrecursorBody(),
                new AgentExpression(),
                new AssertionTagExpression(),
                new BinaryConditionalExpression(),
                new BinaryOperatorExpression(),
                new ClassConstantExpression(),
                new CloneOfExpression(),
                new EntityExpression(),
                new EqualityExpression(),
                new IndexQueryExpression(),
                new InitializedObjectExpression(),
                new KeywordEntityExpression(),
                new KeywordExpression(),
                new ManifestCharacterExpression(),
                new ManifestNumberExpression(),
                new ManifestStringExpression(),
                new NewExpression(),
                new OldExpression(),
                new PrecursorExpression(),
                new PrecursorIndexExpression(),
                new PreprocessorExpression(),
                new QueryExpression(),
                new ResultOfExpression(),
                new UnaryNotExpression(),
                new UnaryOperatorExpression(),
                new IndexerFeature(),
                new AttributeFeature(),
                new ConstantFeature(),
                new CreationFeature(),
                new FunctionFeature(),
                new ProcedureFeature(),
                new PropertyFeature(),
                new AsLongAsInstruction(),
                new AssignmentInstruction(),
                new AttachmentInstruction(),
                new CheckInstruction(),
                new CommandInstruction(),
                new CreateInstruction(),
                new DebugInstruction(),
                new ForLoopInstruction(),
                new IfThenElseInstruction(),
                new IndexAssignmentInstruction(),
                new InspectInstruction(),
                new KeywordAssignmentInstruction(),
                new OverLoopInstruction(),
                new PrecursorIndexAssignmentInstruction(),
                new PrecursorInstruction(),
                new RaiseEventInstruction(),
                new ReleaseInstruction(),
                new ThrowInstruction(),
                new AnchoredType(),
                new FunctionType(),
                new IndexerType(),
                new KeywordAnchoredType(),
                new ProcedureType(),
                new PropertyType(),
                new GenericType(),
                new SimpleType(),
                new TupleType(),
                new AssignmentTypeArgument(),
                new PositionalTypeArgument(),
            };
        }

        [Test]
        public static void TestLanguageInitializers()
        {
            MemberInfo FeatureInfo = typeof(Class).GetMember("EntityName")[0];
            FeatureEntity TestFeatureEntity = new(FeatureInfo);
            FunctionEntity TestFunctionEntity = new(FeatureInfo);
            IndexerEntity TestIndexerEntity = new(FeatureInfo);
            ProcedureEntity TestProcedureEntity = new(FeatureInfo);
            PropertyEntity TestPropertyEntity = new(FeatureInfo);

            SpecializedTypeEntity<Class> TestSpecializedTypeEntity = SpecializedTypeEntity<Class>.Singleton;

            DateAndTime TestDateAndTime = new();
            Event TestEvent = new(isAutoReset: true);

            DetachableReference<Node> TestDetachableReference = new();
            OnceReference<Node> TestOnceReference = new();
            OptionalReference<Node> TestOptionalReference = new();
            StableReference<Node> TestStableReference = new();

            SealableList<Node> TestSealableList = new();
            SealableDictionary<string, Node> TestSealableDictionary = new();
        }
    }
}
